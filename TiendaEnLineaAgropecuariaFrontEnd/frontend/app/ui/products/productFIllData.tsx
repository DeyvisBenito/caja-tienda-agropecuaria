"use client";

import { useParams, useRouter } from "next/navigation";

import {
  Bodegas,
  Inventario,
  InventarioCreacion,
  ProductPost,
  TipoProducto,
} from "@/app/lib/definitions";
import { useValidateToken } from "@/app/lib/useValidateToken";
import { getBodegas, getTiposProductos, updateInventario } from "@/app/lib/api";

import { toast } from "sonner";
import { useForm } from "react-hook-form";
import { useEffect, useState } from "react";
import { useUserRole } from "@/app/lib/decodeToken";

const sleep = (ms: number) => new Promise((resolve) => setTimeout(resolve, ms));

export default function ProductFillData({
  inventario,
  editable,
}: {
  inventario: Inventario;
  editable: boolean;
}) {
  useValidateToken();
  const role = useUserRole();
  const [tipoProductos, setTipoProductos] = useState<TipoProducto[]>([]);
  const [bodegas, setBodegas] = useState<Bodegas[]>([]);

  const params = useParams();
  // Agarra el primer elemento si es que viene un array en el param
  const id = Array.isArray(params.id) ? params.id[0] : params.id;
  if (!id) {
    toast.error("Producto no seleccionado");
    setTimeout(() => {
      router.push("/dashboard/product");
    }, 2000);

    return;
  }

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();

  const router = useRouter();

  const onSubmit = handleSubmit(async (data) => {
    const token = localStorage.getItem("token");
    const button = document.getElementById("submitButton") as HTMLButtonElement;
    button.disabled = true;

    const postInventarioData: InventarioCreacion = {
      Nombre: data.nombre,
      TipoProductoId: data.tipo,
      EstadoId: data.estado,
      BodegaId: data.bodega,
      Marca: data.marca,
      Precio: data.price,
      Foto: data.foto,
      Descripcion: data.description,
      Stock: data.stock,
    };

    try {
      const data = await updateInventario(token, postInventarioData, id);

      if (data === 403) {
        toast.error("No tienes autorización", {
          description: "Inicia sesión, por favor",
        });

        await sleep(2000);
        localStorage.removeItem("token");
        button.disabled = false;
        router.push("/auth/login");
        return;
      }

      toast.success("Producto actualizado del inventario correctamente", {
        description: "Volviendo al dashboard...",
      });

      await sleep(3000);
      button.disabled = false;
      router.push("/dashboard/product");
    } catch (error: any) {
      toast.error(error.message);
      button.disabled = false;
    }
  });

  useEffect(() => {
    const token = localStorage.getItem("token");
    const obterDatos = async () => {
      try {
        const dataTipos = await getTiposProductos(token);
        if (dataTipos === 403) {
          toast.error("No tienes acceso a este recurso", {
            description: "Por favor, inicie sesion",
          });
          await sleep(2000);
          localStorage.removeItem("token");
          router.push("/auth/login");
          return;
        }
        const tipoProductos: TipoProducto[] = dataTipos as TipoProducto[];
        setTipoProductos(tipoProductos);

        const dataBodegas = await getBodegas(token);
        if (dataBodegas === 403) {
          toast.error("No tienes acceso a este recurso", {
            description: "Por favor, inicie sesion",
          });
          await sleep(2000);
          localStorage.removeItem("token");
          router.push("/auth/login");
          return;
        }
        const bodegas: Bodegas[] = dataBodegas as Bodegas[];
        setBodegas(bodegas);
      } catch (error: any) {
        toast.error(error.message);
      }
    };

    obterDatos();
  }, [router]);

  return (
    <form onSubmit={onSubmit}>
      {inventario && (
        <div className="flex flex-col gap-4">
          {/* Nombre */}
          <label htmlFor="nombre" className="font-medium">
            Nombre
          </label>
          <input
            className="px-4 py-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-400 bg-gray-100"
            type="text"
            id="nombre"
            defaultValue={inventario.nombre}
            readOnly={!editable}
            {...register("nombre", {
              required: {
                value: true,
                message: "Name es requerido",
              },
            })}
          />
          {errors.nombre && (
            <span className="text-red-500 text-sm">
              {errors.nombre.message as string}
            </span>
          )}

          {/* Tipo de Producto */}
          <label htmlFor="tipo" className="font-medium mt-4">
            Tipo de Producto:
          </label>
          {editable ? (
            <>
              <select
                id="tipo"
                className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
                {...register("tipo", {
                  required: "El tipo de producto es requerido",
                })}
              >
                <option value={inventario.tipoProductoId}>
                  {inventario.tipoProducto}
                </option>
                {tipoProductos
                  ?.filter((tipo) => tipo.estadoId === 1)
                  .map((tipo) =>
                    tipo.id != inventario.tipoProductoId ? (
                      <option value={tipo.id} key={tipo.id}>
                        {tipo.nombre}
                      </option>
                    ) : null
                  )}
              </select>
              {errors.tipo && (
                <span className="text-red-500 text-sm">
                  {errors.tipo.message as string}
                </span>
              )}
            </>
          ) : (
            <>
              <select
                id="tipo"
                className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
              >
                <option value={inventario.tipoProducto}>
                  {inventario.tipoProducto}
                </option>
              </select>
            </>
          )}

          {/* Estado */}
          <label htmlFor="estado" className="font-medium mt-4">
            Estado:
          </label>
          {editable ? (
            <>
              <select
                id="estado"
                className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
                {...register("estado", { required: "El estado es requerido" })}
              >
                <option value={inventario.estadoId}>{inventario.estado}</option>
                {inventario.estadoId == 1 ? (
                  <option value="2">Inactivo</option>
                ) : (
                  <option value="1">Activo</option>
                )}
              </select>
              {errors.estado && (
                <span className="text-red-500 text-sm">
                  {errors.estado.message as string}
                </span>
              )}
            </>
          ) : (
            <>
              <select
                id="tipo"
                className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
              >
                <option value={inventario.estado}>{inventario.estado}</option>
              </select>
            </>
          )}

          {/* Bodega */}
          <label htmlFor="bodega" className="font-medium mt-4">
            Bodega:
          </label>
          {editable ? (
            <>
              <select
                id="bodega"
                className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
                {...register("bodega", { required: "La bodega es requerida" })}
              >
                <option value={inventario.bodegaId}>{inventario.bodega}</option>
                {bodegas
                  ?.filter((bodega) => bodega.estadoId === 1)
                  .map((bodega) =>
                    inventario.bodegaId != bodega.id ? (
                      <option value={bodega.id} key={bodega.id}>
                        {bodega.nombre}
                      </option>
                    ) : null
                  )}
              </select>
              {errors.bodega && (
                <span className="text-red-500 text-sm">
                  {errors.bodega.message as string}
                </span>
              )}
            </>
          ) : (
            <>
              <select
                id="tipo"
                className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
              >
                <option value={inventario.bodega}>{inventario.bodega}</option>
              </select>
            </>
          )}

          {/* Marca */}
          <label htmlFor="marca" className="font-medium mt-4">
            Marca:
          </label>
          <input
            className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
            type="text"
            id="marca"
            defaultValue={inventario.marca}
            readOnly={!editable}
            {...register("marca", { required: "La marca es requerida" })}
          />
          {errors.marca && (
            <span className="text-red-500 text-sm">
              {errors.marca.message as string}
            </span>
          )}

          {/* Precio */}
          <label htmlFor="price" className="font-medium mt-4">
            Precio:
          </label>
          <input
            className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
            type="number"
            step="0.01"
            id="price"
            min="0.1"
            defaultValue={inventario.precio}
            readOnly={!editable}
            placeholder="1.00"
            {...register("price", { required: "El precio es requerido" })}
          />
          {errors.price && (
            <span className="text-red-500 text-sm">
              {errors.price.message as string}
            </span>
          )}

          {/* Foto */}
          <label className="font-medium mt-4">Foto actual:</label>
          {editable ? (
            <>
              <div className="mb-4 w-full rounded-lg flex justify-center items-center bg-gray-100">
                <img
                  src={inventario.urlFoto}
                  alt={inventario.nombre}
                  className="w-full h-auto max-h-96 object-contain"
                />
              </div>
              <label htmlFor="foto" className="font-medium mt-4">
                Foto nueva:
              </label>
              <input
                type="file"
                id="foto"
                accept="image/*"
                className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
                {...register("foto")}
              />
            </>
          ) : (
            <>
              <div className="mb-4 w-full rounded-lg flex justify-center items-center bg-gray-100">
                <img
                  src={inventario.urlFoto}
                  alt={inventario.nombre}
                  className="w-full h-auto max-h-96 object-contain"
                />
              </div>
            </>
          )}

          {/* Descripción */}
          <label htmlFor="description" className="font-medium mt-4">
            Descripción:
          </label>
          <textarea
            className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
            id="description"
            defaultValue={inventario.descripcion}
            readOnly={!editable}
            {...register("description", {
              required: "La descripción es requerida",
            })}
          />
          {errors.description && (
            <span className="text-red-500 text-sm">
              {errors.description.message as string}
            </span>
          )}

          {/* Stock */}
          {role === "admin" ? (
            <label htmlFor="stock" className="font-medium mt-4">
              Stock:
            </label>
          ) : (
            <label htmlFor="stock" className="font-medium mt-4">
              Unidades disponibles:
            </label>
          )}
          <input
            className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
            type="number"
            step="1"
            min="0"
            id="stock"
            defaultValue={inventario.stock}
            readOnly={!editable}
            placeholder="1"
            {...register("stock", { required: "El stock es requerido" })}
          />
          {errors.stock && (
            <span className="text-red-500 text-sm">
              {errors.stock.message as string}
            </span>
          )}

          {editable && (
            <button
              id="submitButton"
              className="mt-6 w-full bg-blue-500 hover:bg-blue-700 text-white font-semibold py-2 rounded-lg shadow-md transition-colors"
            >
              Actualizar
            </button>
          )}
        </div>
      )}
    </form>
  );
}
