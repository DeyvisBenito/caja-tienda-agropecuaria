"use client";

import { jwtDecode } from 'jwt-decode';
import { useEffect, useState } from 'react';

type JwtPayload = {
    Email: string;
    usuarioId: string;
    role?: string;
    exp: number;
};

export function useUserRole() {
    const [role, setRole] = useState<string | null>(null);

    useEffect(() => {
        try {
            const token = localStorage.getItem("token");
            if (token) {
                const decoded = jwtDecode<JwtPayload>(token);
                if (decoded.role) {
                    setRole(decoded.role);
                }
            }
        } catch (error) {
            setRole(null);
        }
    }, []);

    return role;
}
