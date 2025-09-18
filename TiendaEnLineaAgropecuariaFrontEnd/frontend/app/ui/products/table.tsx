import Image from "next/image";
import {
  UpdateProduct,
  DeleteProduct,
  SeeProduct,
  AddProductoToCart,
} from "@/app/ui/products/buttons";

import { NoSymbolIcon } from "@heroicons/react/24/outline";
import { formatCurrency } from "@/app/lib/utils";
import { Inventario } from "@/app/lib/definitions";
import { useUserRole } from "@/app/lib/decodeToken";

export default function Table({
  inventario,
  onDeleted,
}: {
  inventario: Inventario[];
  onDeleted: (id: number) => void;
}) {
  const role = useUserRole();

  const inventarioAdmin = useUserRole();

  return (
    <div className="mt-6 grid grid-cols-1 gap-4 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-5">
      {inventario?.map((inventari) => (
        <div
          key={inventari.id}
          className="rounded-lg bg-white shadow p-4 flex flex-col"
        >
          {/* Imagen */}
          <div className="mb-4 h-48 w-full overflow-hidden rounded-lg">
            <img
              src={inventari.urlFoto}
              alt={inventari.nombre}
              className="h-full w-full object-cover"
            />
          </div>

          {/* Nombre */}
          <h2 className="text-lg font-semibold mb-1">{inventari.nombre}</h2>

          {/* Estado */}
          <p
            className={`mb-1 font-medium ${
              inventari.estado === "Activo" ? "text-green-600" : "text-red-600"
            }`}
          >
            {inventari.estado}
          </p>

          {/* Marca */}
          <p className="text-gray-500 mb-1">Marca: {inventari.marca}</p>

          {/* Precio */}
          <p className="text-xl font-bold mb-2">
            {formatCurrency(inventari.precio)}
          </p>

          {/* Acciones */}
          <div className="mt-auto flex gap-2 justify-end">
            <SeeProduct id={inventari.id} />
            {role === "admin" ? (
              <>
                <UpdateProduct id={inventari.id} />
                <DeleteProduct id={inventari.id} onDeleted={onDeleted} />
              </>
            ) : inventari.estado === "Activo" ? (
              <AddProductoToCart id={inventari.id} />
            ) : (
              <NoSymbolIcon className="w-5 text-red-500" />
            )}
          </div>
        </div>
      ))}
    </div>
  );
}
