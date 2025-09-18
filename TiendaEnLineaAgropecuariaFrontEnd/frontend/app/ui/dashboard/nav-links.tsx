"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";

import clsx from "clsx";

import { CubeIcon, Squares2X2Icon, PuzzlePieceIcon, BuildingStorefrontIcon  } from "@heroicons/react/24/outline";
import { useUserRole } from "@/app/lib/decodeToken";

// Map of links to display in the side navigation.
// Depending on the size of the application, this would be stored in a database.
const links = [
  { name: "Inventarios", href: "/dashboard/product", icon: CubeIcon, onlyAdmin: false },
  { name: "Categorias", href: "/dashboard/categorias", icon: Squares2X2Icon, onlyAdmin: true },
  { name: "Tipos de Producto", href: "/dashboard/tiposProducto", icon: PuzzlePieceIcon, onlyAdmin: true },
  { name: "Bodegas", href: "/dashboard/bodegas", icon: BuildingStorefrontIcon, onlyAdmin: true }
];

export default function NavLinks() {
  const pathname = usePathname();
  const role = useUserRole();

  const linksVisibles = links.filter(link => {
    if(role === 'admin') return true;

    return !link.onlyAdmin;
  })

  return (
    <>
      {linksVisibles.map((link) => {
        const LinkIcon = link.icon;
        return (
          
          <Link
            key={link.name}
            href={link.href}
            className={clsx(
              "flex h-[48px] grow items-center justify-center gap-2 rounded-md bg-gray-50 p-3 text-sm font-medium hover:text-green-300 md:flex-none md:justify-start md:p-2 md:px-3",
              {
                "bg-sky-100 text-green-600": pathname === link.href,
              }
            )}
          >
            <LinkIcon className="w-6" />
            <p className="hidden md:block">{link.name}</p>
          </Link>
        );
      })}
    </>
  );
}
