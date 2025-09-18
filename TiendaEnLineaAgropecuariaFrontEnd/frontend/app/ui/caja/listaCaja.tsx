"use client";
import Image from "next/image";
import { useRouter } from "next/navigation";

import { useEffect, useState } from "react";
import { Toaster, toast } from "sonner";

import { useValidateToken } from "@/app/lib/useValidateToken";

// Productos de ejemplo
const productos = [
  {
    id: 1,
    nombre: "Camiseta básica",
    precio: 120,
    imagen: "/camiseta.jpg",
  },
  {
    id: 2,
    nombre: "Zapatillas deportivas",
    precio: 450,
    imagen: "/zapatillas.jpg",
  },
  {
    id: 3,
    nombre: "Gorra negra",
    precio: 80,
    imagen: "/gorra.jpg",
  },
];

const sleep = (ms: number) => new Promise((resolve) => setTimeout(resolve, ms));
export default function ListaCaja() {
  useValidateToken();
  const router = useRouter();

  const [cantidades, setCantidades] = useState<{ [id: number]: number }>({});

  // Cambiar cantidad y llamar API
  const actualizarCantidad = async (id: number, cambio: number) => {
    const nuevaCantidad = Math.max(0, (cantidades[id] || 0) + cambio);

    setCantidades({
      ...cantidades,
      [id]: nuevaCantidad,
    });

    // Llamada a la API
    try {
      await fetch("/api/cart/update", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ productId: id, quantity: nuevaCantidad }),
      });
    } catch (error) {
      console.error("Error actualizando carrito:", error);
    }

    useEffect(() => {
      const token = localStorage.getItem('token');
      if(!token){

      }
    }), [router];

  };

  return (
    <div className="grid grid-cols-1 md:grid-cols-5 gap-6 p-6">
      <Toaster position="top-center" richColors />
      {productos.map((producto) => (
        <div
          key={producto.id}
          className="rounded-xl border p-4 shadow-md bg-white flex flex-col items-center"
        >
          <Image
            src={producto.imagen}
            alt={producto.nombre}
            width={150}
            height={150}
            className="rounded-lg object-cover"
          />
          <h3 className="mt-3 text-lg font-semibold">{producto.nombre}</h3>
          <p className="text-green-600 font-bold">Q {producto.precio}</p>

          <div className="flex items-center gap-3 mt-3">
            <button
              onClick={() => actualizarCantidad(producto.id, -1)}
              className="px-3 py-1 bg-red-500 text-white rounded-lg hover:bg-red-600"
            >
              -
            </button>
            <span className="min-w-[30px] text-center">
              {cantidades[producto.id] || 0}
            </span>
            <button
              onClick={() => actualizarCantidad(producto.id, 1)}
              className="px-3 py-1 bg-green-500 text-white rounded-lg hover:bg-green-600"
            >
              +
            </button>
          </div>
        </div>
      ))}
    </div>
  );
}
