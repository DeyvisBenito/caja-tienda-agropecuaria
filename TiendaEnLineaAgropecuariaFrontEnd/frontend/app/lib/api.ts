import { Product, User, Token, ProductPost, ProductsPaginated, LoginConEmailI, Categoria, Inventario, ReseteoPasswordIn, ReseteoPasswordResponse, CredencialesResetearPassword, TipoProducto, Bodegas, InventarioCreacion, CarritoSuccess } from './definitions'

// Change URL_BASE instead your Backend URL_BASE
const URL_BASE: string = process.env.NEXT_PUBLIC_URL_BASE || 'http://localhost:3000';


export async function registro(objeto: LoginConEmailI): Promise<Token> {
    try {
        const result = await fetch(`${URL_BASE}/v1/Usuarios/registroConEmail`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(objeto),
        });

        const data = await result.json();

        if (!result.ok) {
            const mensajeError = data.errors && data.errors[""] ? data.errors[""][0] : "Error al registrarse";
            throw new Error(mensajeError);
        }

        return data as Token;
    } catch (error: any) {
        const mensaje = error.message || "Error desconocido";
        throw new Error(mensaje);
    }
}

// Fetch of login
export async function loginConEmail(credentials: LoginConEmailI): Promise<Token> {
    try {
        const result = await fetch(`${URL_BASE}/v1/Auth/loginConEmail`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(credentials),
        });

        const data = await result.json();

        if (!result.ok) {
            const mensajeError = data.errors && data.errors[""] ? data.errors[""][0] : "Error al loguearse";
            throw new Error(mensajeError);
        }

        return data as Token;
    } catch (error: any) {
        const mensaje = error.message || "Error desconocido";
        throw new Error(mensaje);
    }
}

export async function recuperarPassword(email: string): Promise<{ mensaje: string }> {
    try {
        const result = await fetch(`${URL_BASE}/v1/Auth/recuperar-password`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email }),
        });

        const data = await result.json();

        if (!result.ok) {
            const mensajeError =
                data.errors && data.errors[""]
                    ? data.errors[""][0]
                    : data.message || "Error al recuperar la contraseña";
            throw new Error(mensajeError);
        }

        return data;
    } catch (error: any) {
        const mensaje = error.message || "Error desconocido";
        throw new Error(mensaje);
    }
}

export async function validarTokenResetearPassword(
    credenciales: CredencialesResetearPassword
): Promise<boolean> {
    try {
        const res = await fetch(
            `${URL_BASE}/v1/Auth/validar-token-resetPassword`,
            {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(credenciales),
            }
        );

        const data = await res.json();

        if (!res.ok) {
            return false;
        }

        return data as boolean;
    } catch (error: any) {
        throw new Error(error.message || "Error desconocido");
    }
}

export async function resetearPassword(
    credenciales: ReseteoPasswordIn
): Promise<ReseteoPasswordResponse> {
    try {
        const res = await fetch(
            `${URL_BASE}/v1/Auth/reset-password`,
            {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(credenciales),
            }
        );

        const data = await res.json();

        if (!res.ok) {
            const mensajeError =
                data.errors?.["dto"]?.[0] ||
                data.errors?.Email?.[0] ||
                data.errors?.Token?.[0] ||
                data.errors?.NuevoPassword?.[0] ||
                data.message ||
                "Error al resetear la contraseña";
            throw new Error(mensajeError);
        }

        return data as ReseteoPasswordResponse;
    } catch (error: any) {
        throw new Error(error.message || "Error desconocido");
    }
}

// Fetch validate Token
export async function validateToken(token: string): Promise<boolean> {
    const result = await fetch(`${URL_BASE}/v1/Auth/validarToken?token=${token}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    if (!result.ok) {
        return false;
    }

    return true;
}

// Fetch get products
export const getInventario = async (token: string | null): Promise<Inventario[] | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Inventarios`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    const data: Inventario[] = await resp.json();

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar los inventarios");
    }

    return data;
}

// Fetch get product with Id
export const getInventarioById = async (token: string | null, id: string): Promise<Inventario | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Inventarios/${id}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    const data: Inventario = await resp.json();
    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al obtener el producto");
    }

    return data;
}

// Do a Post of Inventario
export const postInventario = async (token: string | null, inventario: InventarioCreacion): Promise<void | number> => {
    const formData = new FormData();

    formData.append("Nombre", inventario.Nombre);
    formData.append("TipoProductoId", String(inventario.TipoProductoId));
    formData.append("EstadoId", String(inventario.EstadoId));
    formData.append("BodegaId", String(inventario.BodegaId));
    formData.append("Marca", inventario.Marca);
    formData.append("Precio", String(inventario.Precio));
    formData.append("Descripcion", inventario.Descripcion || "");
    formData.append("Stock", String(inventario.Stock));
    formData.append("Foto", inventario.Foto![0]);


    const resp = await fetch(`${URL_BASE}/v1/Inventarios`, {
        method: "POST",
        headers: {
            Authorization: `Bearer ${token}`
        },
        body: formData
    });

    const data = await resp.json();

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al agregar producto al inventario";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return data;
}

// Do a Update of product
export const updateInventario = async (token: string | null, inventario: InventarioCreacion, id: string): Promise<void | number> => {
    const formData = new FormData();

    formData.append("Nombre", inventario.Nombre);
    formData.append("TipoProductoId", String(inventario.TipoProductoId));
    formData.append("EstadoId", String(inventario.EstadoId));
    formData.append("BodegaId", String(inventario.BodegaId));
    formData.append("Marca", inventario.Marca);
    formData.append("Precio", String(inventario.Precio));
    formData.append("Descripcion", inventario.Descripcion || "");
    formData.append("Stock", String(inventario.Stock));

    if (inventario.Foto) {
        formData.append("Foto", inventario.Foto[0]);
    }

    const resp = await fetch(`${URL_BASE}/v1/Inventarios/${id}`, {
        method: "PUT",
        headers: {
            Authorization: `Bearer ${token}`
        },
        body: formData
    });

    const data = await resp.json();

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al actualizar producto del inventario";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return data;
}



// Do a delete with Id
export const deleteInventario = async (token: string | null, id: number): Promise<void | number> => {
    const resp = await fetch(`${URL_BASE}/v1/Inventarios/${id}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    const data = await resp.json();

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al eliminar el producto del inventario";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return data;
}


// Categorias
export const getCategorias = async (token: string | null): Promise<Categoria[] | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Categorias`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    const data: Categoria[] = await resp.json();

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar Categorias");
    }

    return data;
}

// Get tiposproductos
export const getTiposProductos = async (token: string | null): Promise<TipoProducto[] | number> => {

    const resp = await fetch(`${URL_BASE}/v1/TiposProducto`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    const data: TipoProducto[] = await resp.json();

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar los Tipos de Productos");
    }

    return data;
}

// Get Bodegas
export const getBodegas = async (token: string | null): Promise<Bodegas[] | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Bodegas`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    const data: Bodegas[] = await resp.json();

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar las bodegas");
    }

    return data;
}

// Get Carrito if it exists
export const getCart = async (token: string | null, userId: string): Promise<CarritoSuccess | number> => {
    const resp = await fetch(`${URL_BASE}/v1/Carrito`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    const data: Bodegas[] = await resp.json();

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar las bodegas");
    }

    return data;
}
