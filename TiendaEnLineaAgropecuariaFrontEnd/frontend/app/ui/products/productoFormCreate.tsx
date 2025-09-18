"use client";

import Link from "next/link";
import { useRouter } from "next/navigation";
import { useForm } from "react-hook-form";
import { Toaster, toast } from "sonner";
import { useValidateToken } from "@/app/lib/useValidateToken";
import { getBodegas, getTiposProductos, postInventario } from "@/app/lib/api";
import {
  Bodegas,
  InventarioCreacion,
  ProductPost,
  TipoProducto,
} from "@/app/lib/definitions";
import { useEffect, useState } from "react";

const sleep = (ms: number) => new Promise((resolve) => setTimeout(resolve, ms));

export default function ProductFormCreate() {
  useValidateToken();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();

  const [tipoProductos, setTipoProductos] = useState<TipoProducto[]>([]);
  const [bodegas, setBodegas] = useState<Bodegas[]>([]);
  const router = useRouter();

  const onSubmit = handleSubmit(async (data) => {
    const button = document.getElementById("submitButton") as HTMLButtonElement;
    button.disabled = true;

    const token = localStorage.getItem("token");
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
      const data = await postInventario(token, postInventarioData);

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

      toast.success("Producto agregado al inventario correctamente", {
        description: "Volviendo al dashboard...",
      });

      await sleep(2000);
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
      <div className="flex flex-col gap-4">
        {/* Nombre */}
        <label htmlFor="nombre" className="font-medium">
          Nombre:
        </label>
        <input
          className="px-4 py-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-400 bg-gray-100"
          type="text"
          id="nombre"
          {...register("nombre", { required: "El nombre es requerido" })}
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
        <select
          id="tipo"
          className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
          {...register("tipo", {
            required: "El tipo de producto es requerido",
          })}
        >
          <option value="">
            Seleccione un tipo
          </option>
          {tipoProductos
            ?.filter((tipo) => tipo.estadoId === 1)
            .map((tipo) => (
              <option value={tipo.id} key={tipo.id}>
                {tipo.nombre}
              </option>
            ))}
        </select>
        {errors.tipo && (
          <span className="text-red-500 text-sm">
            {errors.tipo.message as string}
          </span>
        )}

        {/* Estado */}
        <label htmlFor="estado" className="font-medium mt-4">
          Estado:
        </label>
        <select
          id="estado"
          className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
          {...register("estado", { required: "El estado es requerido" })}
        >
          <option value="">
            Seleccione un estado
          </option>
          <option value="1">Activo</option>
          <option value="2">Inactivo</option>
        </select>
        {errors.estado && (
          <span className="text-red-500 text-sm">
            {errors.estado.message as string}
          </span>
        )}

        {/* Bodega */}
        <label htmlFor="bodega" className="font-medium mt-4">
          Bodega:
        </label>
        <select
          id="bodega"
          className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
          {...register("bodega", { required: "La bodega es requerida" })}
        >
          <option value="">
            Seleccione una bodega de almacenaje
          </option>
          {bodegas
            ?.filter((bodega) => bodega.estadoId === 1)
            .map((bodega) => (
              <option value={bodega.id} key={bodega.id}>
                {bodega.nombre}
              </option>
            ))}
        </select>
        {errors.bodega && (
          <span className="text-red-500 text-sm">
            {errors.bodega.message as string}
          </span>
        )}

        {/* Marca */}
        <label htmlFor="marca" className="font-medium mt-4">
          Marca:
        </label>
        <input
          className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
          type="text"
          id="marca"
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
          min="0.1"
          id="price"
          placeholder="1.00"
          {...register("price", { required: "El precio es requerido" })}
        />
        {errors.price && (
          <span className="text-red-500 text-sm">
            {errors.price.message as string}
          </span>
        )}

        {/* Foto */}
        <label htmlFor="foto" className="font-medium mt-4">
          Foto:
        </label>
        <input
          type="file"
          id="foto"
          accept="image/*"
          className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
          {...register("foto", { required: "La foto es requerida" })}
        />
        {errors.foto && (
          <span className="text-red-500 text-sm">
            {errors.foto.message as string}
          </span>
        )}

        {/* Descripción */}
        <label htmlFor="description" className="font-medium mt-4">
          Descripción:
        </label>
        <textarea
          className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
          id="description"
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
        <label htmlFor="stock" className="font-medium mt-4">
          Stock:
        </label>
        <input
          className="px-4 py-2 border border-gray-300 rounded bg-gray-100"
          type="number"
          step="1"
          min="0"
          id="stock"
          placeholder="1"
          {...register("stock", { required: "El stock es requerido" })}
        />
        {errors.stock && (
          <span className="text-red-500 text-sm">
            {errors.stock.message as string}
          </span>
        )}

        {/* Botones */}
        <button
          id="submitButton"
          className="mt-6 w-full bg-blue-500 hover:bg-blue-700 text-white font-semibold py-2 rounded-lg shadow-md transition-colors"
        >
          Crear
        </button>

        <Link
          href="/dashboard/product"
          className="w-full text-center bg-gray-200 hover:bg-gray-300 text-gray-800 font-semibold py-2 rounded-lg shadow-md transition-colors inline-block"
        >
          Cancelar
        </Link>
      </div>
    </form>
  );
}
