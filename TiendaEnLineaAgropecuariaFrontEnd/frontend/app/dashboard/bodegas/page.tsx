"use client";

import { useRouter } from "next/navigation";

import { CreateProduct } from "@/app/ui/products/buttons";
import { useValidateToken } from "@/app/lib/useValidateToken";
import { getBodegas } from "@/app/lib/api";

import { useEffect, useState } from "react";
import { Toaster, toast } from "sonner";
import { Bodegas } from "@/app/lib/definitions";
import TablaBodegas from "@/app/ui/bodegas/tablaBodegas";

const sleep = (ms: number) => new Promise((resolve) => setTimeout(resolve, ms));

export default function BodegasPage() {
  const router = useRouter();
  useValidateToken();

  // States for total and current page
  //const [total, setTotal] = useState(0);
  //const [page, setPage] = useState(1);
  // Products per page
  //const sizeProducts: number = 5;

  const [respBodegas, setRespBodegas] = useState<Bodegas[]>([]);

  useEffect(() => {
    const token = localStorage.getItem("token");
    const fetchBodegas = async () => {
      try {
        const data = await getBodegas(token);
        if (data === 403) {
          toast.error("No tienes acceso a este recurso", {
            description: "Por favor, inicie sesion",
          });
          await sleep(2000);
          localStorage.removeItem("token");
          router.push("/auth/login");
          return;
        }
        const bodegas: Bodegas[] = data as Bodegas[];
        setRespBodegas(bodegas);
      } catch (error: any) {
        toast.error(error.message);
      }
    };

    fetchBodegas();
  }, [router]);

  return (
    <div className="w-full">
      <Toaster position="top-center" richColors />
      <div className="flex w-full items-center justify-between">
        <h1 className={`text-2xl`}>Bodegas</h1>
      </div>
      <div className="mt-4 flex items-center gap-2 md:mt-8 text-end justify-end">
        <CreateProduct />
      </div>
      <div>
        <TablaBodegas
          bodegas={respBodegas}
          onDeleted={(deletedId) => {
            setRespBodegas((prev: Bodegas[]) =>
              prev.filter((p) => p.id !== deletedId)
            );
          }}
        />
      </div>
    </div>
  );
}
