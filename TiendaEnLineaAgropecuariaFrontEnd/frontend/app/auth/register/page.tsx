"use client";

import Link from "next/link";
import { useRouter } from "next/navigation";

import { useForm } from "react-hook-form";
import { useState } from "react";
import { Toaster, toast } from "sonner";

import { LoginConEmailI } from "../../lib/definitions";
import { registro } from "@/app/lib/api";

export default function RegisterPage() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();


  const router = useRouter();

  const onSubmit = handleSubmit(async (data) => {
    if (data.password !== data.confirmPassword) {
      toast.error('Las contraseñas no coinciden');
      return;
    }

    const userLogin: LoginConEmailI = {
      Email: data.email,
      Password: data.password,
    };

    try {
      const data = await registro(userLogin);
      localStorage.setItem("token", data.token);
      router.push("/dashboard/product");
    } catch (error: any) {
      toast.error(error.message);
    }
  });

  return (
    <div className="bg-white rounded-lg shadow-lg p-8 sm:p-10 w-full">
      <Toaster position="top-center" richColors />
      <h1 className="text-3xl sm:text-4xl font-bold mb-6 text-center">
        Register
      </h1>
      <form onSubmit={onSubmit}>
        <div className="flex flex-col gap-4">
          <label htmlFor="email" className="font-medium">
            Correo electrónico
          </label>
          <input
            className="px-4 py-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-400 bg-gray-100"
            type="email"
            id="email"
            {...register("email", {
              required: {
                value: true,
                message: "El Correo Electrónico es requerido",
              },
            })}
          />

          {errors.email && (
            <span className="text-red-500 text-sm">
              {errors.email.message as string}
            </span>
          )}

          <label htmlFor="password" className="font-medium mt-4">
            Contraseña
          </label>
          <input
            className="px-4 py-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-400 bg-gray-100"
            type="password"
            id="password"
            {...register("password", {
              required: {
                value: true,
                message: "La contraseña es requerida",
              },
            })}
          />
          {errors.password && (
            <span className="text-red-500 text-sm">
              {errors.password.message as string}
            </span>
          )}

          <label htmlFor="confirmPassword" className="font-medium mt-4">
            Confirmar Contraseña
          </label>
          <input
            className="px-4 py-2 border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-400 bg-gray-100"
            type="password"
            id="confirmPassword"
            {...register("confirmPassword", {
              required: {
                value: true,
                message: "El confirmar contraseña es requerida",
              },
            })}
          />
          {errors.confirmPassword && (
            <span className="text-red-500 text-sm">
              {errors.confirmPassword.message as string}
            </span>
          )}

          <button className="mt-6 w-full bg-blue-500 hover:bg-blue-700 text-white font-semibold py-2 rounded-lg shadow-md transition-colors">
            Register
          </button>

          <Link
            href="/auth/login"
            className="w-full text-center bg-gray-200 hover:bg-gray-300 text-gray-800 font-semibold py-2 rounded-lg shadow-md transition-colors inline-block"
          >
            Cancel
          </Link>
        </div>
      </form>
    </div>
  );
}
