import {
  UpdateProduct,
  DeleteProduct,
  SeeProduct,
} from "@/app/ui/products/buttons";
import { Bodegas } from "@/app/lib/definitions";

export default function TablaBodegas({
  bodegas,
  onDeleted,
}: {
  bodegas: Bodegas[];
  onDeleted: (id: number) => void;
}) {
  return (
    <div className="mt-6 flow-root">
      <div className="inline-block min-w-full align-middle">
        <div className="rounded-lg bg-gray-50 p-2 md:pt-0">
          <div className="md:hidden">
            {bodegas?.map((bodega) => (
              <div
                key={bodega.id}
                className="mb-2 w-full rounded-md bg-white p-4"
              >
                <div className="flex items-center justify-between border-b pb-4">
                  <div>
                    <div className="mb-2 flex items-center">
                      <p>{bodega.id}</p>
                    </div>
                    <p className="text-sm text-gray-500">{bodega.nombre}</p>
                  </div>
                  <p>{bodega.nombre} </p>
                </div>
                <div className="flex w-full items-center justify-between pt-4">
                  <div>
                    <p
                      className={`text-xl font-medium ${
                        bodega.estado === "Activo"
                          ? "text-green-600"
                          : "text-red-600"
                      }`}
                    >
                      {bodega.estado}
                    </p>
                  </div>
                  <div className="flex justify-end gap-2">
                    <SeeProduct id={bodega.id} />
                    <UpdateProduct id={bodega.id} />
                    <DeleteProduct id={bodega.id} onDeleted={onDeleted} />
                  </div>
                </div>
              </div>
            ))}
          </div>
          <table className="hidden min-w-full text-gray-900 md:table">
            <thead className="rounded-lg text-left text-sm font-normal">
              <tr>
                <th scope="col" className="px-4 py-5 font-medium sm:pl-6">
                  Id
                </th>
                <th scope="col" className="px-3 py-5 font-medium">
                  Nombre
                </th>
                <th scope="col" className="px-3 py-5 font-medium">
                  Estado
                </th>
                <th scope="col" className="relative py-3 pl-6 pr-3">
                  <span className="sr-only">Edit</span>
                </th>
              </tr>
            </thead>
            <tbody className="bg-white">
              {bodegas?.map((bodega) => (
                <tr
                  key={bodega.id}
                  className="w-full border-b py-3 text-sm last-of-type:border-none [&:first-child>td:first-child]:rounded-tl-lg [&:first-child>td:last-child]:rounded-tr-lg [&:last-child>td:first-child]:rounded-bl-lg [&:last-child>td:last-child]:rounded-br-lg"
                >
                  <td className="whitespace-nowrap py-3 pl-6 pr-3">
                    <div className="flex items-center gap-3">
                      <p>{bodega.id}</p>
                    </div>
                  </td>
                  <td className="whitespace-nowrap px-3 py-3">
                    {bodega.nombre}
                  </td>
                  <td
                    className={`whitespace-nowrap px-3 py-3 ${
                      bodega.estado === "Activo"
                        ? "text-green-600"
                        : "text-red-600"
                    }`}
                  >
                    {bodega.estado}
                  </td>
                  <td className="whitespace-nowrap py-3 pl-6 pr-3">
                    <div className="flex justify-end gap-3">
                      <SeeProduct id={bodega.id} />
                      <UpdateProduct id={bodega.id} />
                      <DeleteProduct id={bodega.id} onDeleted={onDeleted} />
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}
