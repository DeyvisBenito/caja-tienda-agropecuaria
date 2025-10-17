import {
    Product, User, Token, ProductPost, ProductsPaginated, LoginConEmailI,
    Categoria, Inventario, ReseteoPasswordIn, ReseteoPasswordResponse,
    CredencialesResetearPassword, TipoProducto, Sucursal, InventarioCreacion, Carrito,
    CarritoDetails,
    DetalleUpdateResult,
    CarritoDetCreacion,
    CategoriaCreacion,
    TipoProductoCreacion,
    TipoMedida,
    Compra,
    Proveedor,
    ProveedorCreacion,
    CompraUpdate,
    CompraCreacion,
    CompraCreacionResp,
    UnidadMedida,
    DetalleCompra,
    DetalleCompraCreacion,
    UsuarioGet,
    UsuarioUpdate,
    Cliente,
    ClienteCreacion
} from './definitions'

// Change URL_BASE instead your Backend URL_BASE
const URL_BASE: string = process.env.NEXT_PUBLIC_URL_BASE || 'http://localhost:3000';

// Usuarios
// Registrar usuario
export async function registro(token: string | null, credenciales: LoginConEmailI): Promise<Token | number> {
    try {
        const result = await fetch(`${URL_BASE}/v1/Usuarios/registroConEmail`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${token}`
            },
            body: JSON.stringify(credenciales),
        });

        if (result.status === 403 || result.status === 401) {
            return 403;
        }

        if (!result.ok) {
            const data = await result.json();
            const mensajeError = data.errors && data.errors[""] ? data.errors[""][0] : "Error al registrarse";
            throw new Error(mensajeError);
        }
        const data = await result.json();

        return data as Token;
    } catch (error: any) {
        const mensaje = error.message || "Error desconocido";
        throw new Error(mensaje);
    }
}

// Get Usuarios
export const getUsuarios = async (token: string | null): Promise<UsuarioGet[] | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Usuarios`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar los Usuarios");
    }

    const data: UsuarioGet[] = await resp.json();
    return data;
}

// Get Usuario by id
export const getUsuarioById = async (token: string | null, id: string): Promise<UsuarioGet | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Usuarios/${id}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al obtener el usuario";
        const data = await resp.json();
        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }
    const data: UsuarioGet = await resp.json();

    return data;
}

// Get Usuario by Email solo true o false
export const getUsuarioByEmailBool = async (token: string | null, email: string): Promise<boolean | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Usuarios/byemailbool/${email}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403 || resp.status === 401) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al obtener el usuario";
        const data = await resp.json();
        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }
    const data: boolean = await resp.json();

    return data;
}

// Update Usuario
export const updateUsuario = async (token: string | null, id: string, usuarioUp: UsuarioUpdate)
    : Promise<void | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Usuarios/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        },
        body: JSON.stringify(usuarioUp)
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        const data = await resp.json();
        let mensajeError = "Error al actualizar el usuario";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

//delete Usuario with Id
export const deleteUsuario = async (token: string | null, id: string): Promise<void | number> => {
    const resp = await fetch(`${URL_BASE}/v1/Usuarios/${id}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    if (resp.status === 403 || resp.status === 401) {
        return 403;
    }
    if (!resp.ok) {
        const data = await resp.json();
        let mensajeError = "Error al eliminar el Usuario";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
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

// Inventarios
// get inventarios
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

// get inventario with Id
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
        let mensajeError = "Error al obtener el producto";
        const data = await resp.json();
        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return data;
}

// get inventario with Codigo
export const getInventarioByCodigo = async (token: string | null, codigo: string | undefined): Promise<Inventario | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Inventarios/bycodigo/${codigo}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al obtener el producto";
        const data = await resp.json();
        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    const data: Inventario = await resp.json();
    return data;
}

// Do a Post of Inventario
export const postInventario = async (token: string | null, inventario: InventarioCreacion): Promise<void | number> => {
    const formData = new FormData();

    formData.append("Codigo", inventario.Codigo);
    formData.append("Nombre", inventario.Nombre);
    formData.append("TipoProductoId", String(inventario.TipoProductoId));
    formData.append("EstadoId", String(inventario.EstadoId));
    formData.append("Marca", inventario.Marca);
    formData.append("Descripcion", inventario.Descripcion || "");
    formData.append("UnidadMedidaId", String(inventario.UnidadMedidaId));
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
    formData.append("Marca", inventario.Marca);
    formData.append("Descripcion", inventario.Descripcion || "");

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

// Fetch get Categoria with Id
export const getCategoriaById = async (token: string | null, id: string): Promise<Categoria | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Categorias/${id}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al obtener la categoria";
        const data = await resp.json();
        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }
    const data: Categoria = await resp.json();
    return data;
}

// Post a categorias
export const postCategorias = async (token: string | null, categoria: CategoriaCreacion): Promise<void | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Categorias`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        },
        body: JSON.stringify(categoria)
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al agregar la categoria";
        const data = await resp.json();
        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

// Update categoria
export const updateCategoria = async (token: string | null, id: string, categoriaUpdate: CategoriaCreacion)
    : Promise<void | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Categorias/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        },
        body: JSON.stringify(categoriaUpdate)
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        const data = await resp.json();
        let mensajeError = "Error al actualizar la categoria";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

//delete categoria with Id
export const deleteCategoria = async (token: string | null, id: number): Promise<void | number> => {
    const resp = await fetch(`${URL_BASE}/v1/Categorias/${id}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        const data = await resp.json();
        let mensajeError = "Error al eliminar la categoria";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

// TiposProducto
// Get tiposproductos
export const getTiposProductos = async (token: string | null): Promise<TipoProducto[] | number> => {

    const resp = await fetch(`${URL_BASE}/v1/TiposProducto`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar los Tipos de Productos");
    }

    const data: TipoProducto[] = await resp.json();
    return data;
}

// Get Tipo Producto by id
export const getTipoProductoById = async (token: string | null, id: string | undefined | number): Promise<TipoProducto | number> => {

    const resp = await fetch(`${URL_BASE}/v1/TiposProducto/${id}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al obtener el producto";
        const data = await resp.json();
        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }
    const data: TipoProducto = await resp.json();

    return data;
}

// Post a TiposProducto
export const postTipoProducto = async (token: string | null, tipoProducto: TipoProductoCreacion): Promise<void | number> => {

    const resp = await fetch(`${URL_BASE}/v1/TiposProducto`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        },
        body: JSON.stringify(tipoProducto)
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al agregar el Tipo de Producto";
        const data = await resp.json();
        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

// Update TipoProducto
export const updateTipoProducto = async (token: string | null, id: string, tipoProductoUpdate: TipoProductoCreacion)
    : Promise<void | number> => {

    const resp = await fetch(`${URL_BASE}/v1/TiposProducto/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        },
        body: JSON.stringify(tipoProductoUpdate)
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        const data = await resp.json();
        let mensajeError = "Error al actualizar el Tipo de Producto";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

//delete Tipo Producto with Id
export const deleteTipoProducto = async (token: string | null, id: number): Promise<void | number> => {
    const resp = await fetch(`${URL_BASE}/v1/TiposProducto/${id}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        const data = await resp.json();
        let mensajeError = "Error al eliminar el Tipo de Producto";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

// Get Carrito if it exists
export const getCart = async (token: string | null): Promise<Carrito | number> => {
    const resp = await fetch(`${URL_BASE}/v1/Carrito`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    const data: Carrito = await resp.json();

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar las bodegas");
    }

    return data;
}

// Borrar esto
export const getBodegas = async (token: string | null): Promise<Carrito | number> => {
    const resp = await fetch(`${URL_BASE}/v1/Carrito`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    const data: Carrito = await resp.json();

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar las bodegas");
    }

    return data;
}

// Get carrito details
export const getCartDetails = async (token: string | null): Promise<CarritoDetails[] | number> => {

    const resp = await fetch(`${URL_BASE}/v1/CarritoDetalle`, {
        method: "GET",
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
        let mensajeError = "Error al obtener los productos de la caja";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return data;
}

// Update carrito details
export const updateCartDetails = async (token: string | null, id: number, carritoDetUpdate: CarritoDetCreacion)
    : Promise<DetalleUpdateResult | number> => {

    const resp = await fetch(`${URL_BASE}/v1/CarritoDetalle/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        },
        body: JSON.stringify(carritoDetUpdate)
    });

    const data = await resp.json();

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al actualizar el producto de la caja";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return data;
}

// Post a product to carrito detail
export const postCartDetail = async (token: string | null, detalle: CarritoDetCreacion): Promise<void | number> => {

    const resp = await fetch(`${URL_BASE}/v1/CarritoDetalle`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        },
        body: JSON.stringify(detalle)
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al agregar producto a la caja";
        const data = await resp.json();
        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}


// Tipos de Medida
export const getTiposMedida = async (token: string | null): Promise<TipoMedida[] | number> => {

    const resp = await fetch(`${URL_BASE}/v1/TiposMedida`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    const data: TipoMedida[] = await resp.json();

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar los Tipos de Medida");
    }

    return data;
}

// Sucursales
// Get Sucursales
export const getSucursales = async (token: string | null): Promise<Sucursal[] | number> => {
    const resp = await fetch(`${URL_BASE}/v1/Sucursales`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar las sucursales");
    }

    const data: Sucursal[] = await resp.json();
    return data;
}

// Get Sucursal By Id
export const getSucursalById = async (token: string | null, id: string): Promise<Sucursal | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Sucursales/${id}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al obtener la sucursal";
        const data = await resp.json();
        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }
    const data: Sucursal = await resp.json();
    return data;
}

// Compras
// Get Compras
export const getCompras = async (token: string | null): Promise<Compra[] | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Compras`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar el historial de compras");
    }
    const data: Compra[] = await resp.json();

    return data;
}

// Post a Compra
export const postCompra = async (token: string | null, compra: CompraCreacion): Promise<CompraCreacionResp | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Compras`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        },
        body: JSON.stringify(compra)
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al agregar la compra";
        const data = await resp.json();
        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }
    const data: CompraCreacionResp = await resp.json();
    return data;
}

// Get Compra by id
export const getCompra = async (token: string | null, id: string):
    Promise<Compra | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Compras/${id}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }

    if (!resp.ok) {
        let mensajeError = "Error al obtener la compra";
        const data = await resp.json();
        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }
    const data: Compra = await resp.json();

    return data;
}

//Confirmar compra
export const procesarCompra = async (token: string | null, idCompra: string): Promise<void | number> => {
    const resp = await fetch(`${URL_BASE}/v1/Compras/${idCompra}/procesar`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        const data = await resp.json();
        let mensajeError = "Error al procesar la compra";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

// Update Compra (solo enunciados principales)
export const updateCompra = async (token: string | null, id: string, CompraCreacion: CompraUpdate)
    : Promise<void | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Compras/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        },
        body: JSON.stringify(CompraCreacion)
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        const data = await resp.json();
        let mensajeError = "Error al actualizar la compra";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

// Delete compra (logico)
export const deleteCompra = async (token: string | null, id: number): Promise<void | number> => {
    const resp = await fetch(`${URL_BASE}/v1/Compras/${id}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });


    if (resp.status === 403) {
        return 403;
    }

    if (!resp.ok) {
        const data = await resp.json();
        let mensajeError = "Error al eliminar la compra";
        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

//delete Cancelar compra eliminado permanente
export const cancelCompra = async (token: string | null, idCompra: string): Promise<void | number> => {
    const resp = await fetch(`${URL_BASE}/v1/Compras/cancelCompra/${idCompra}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        const data = await resp.json();
        let mensajeError = "Error al cancelar la compra";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

// Proveedores
// Get Proveedores
export const getProveedores = async (token: string | null): Promise<Proveedor[] | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Proveedores`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar los Proveedores");
    }
    const data: Proveedor[] = await resp.json();

    return data;
}

// Post a Proveedores
export const postProveedor = async (token: string | null, proveedor: ProveedorCreacion): Promise<void | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Proveedores`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        },
        body: JSON.stringify(proveedor)
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al agregar el Proveedor";
        const data = await resp.json();
        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

// Get Proveedor by id
export const getProveedor = async (token: string | null, id: string): Promise<Proveedor | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Proveedores/${id}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al obtener el proveedor";
        const data = await resp.json();
        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }
    const data: Proveedor = await resp.json();

    return data;
}

// Update Proveedor
export const updateProveedor = async (token: string | null, id: string, proveedor: ProveedorCreacion)
    : Promise<void | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Proveedores/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        },
        body: JSON.stringify(proveedor)
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        const data = await resp.json();
        let mensajeError = "Error al actualizar el Proveedor";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

//delete proveedor with Id
export const deleteProveedor = async (token: string | null, id: number): Promise<void | number> => {
    const resp = await fetch(`${URL_BASE}/v1/Proveedores/${id}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        const data = await resp.json();
        let mensajeError = "Error al eliminar el Proveedor";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

// UnidadesMedida
// Get UnidadesMedida
export const getUnidadesMedida = async (token: string | null): Promise<UnidadMedida[] | number> => {

    const resp = await fetch(`${URL_BASE}/v1/UnidadesMedida`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar los Tipos de Productos");
    }

    const data: UnidadMedida[] = await resp.json();
    return data;
}

// Detalles de compra
// Get detalles de compra
export const getDetallesCompra = async (token: string | null, compraId: string): Promise<DetalleCompra[] | number> => {

    const resp = await fetch(`${URL_BASE}/v1/DetalleCompra/bycompra/${compraId}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar los detalles de la compra");
    }

    const data: DetalleCompra[] = await resp.json();
    return data;
}

// Get detalles de compra
export const getDetalleCompraById = async (token: string | null, idCompra: string | undefined, idDet: string): Promise<DetalleCompra | number> => {

    const resp = await fetch(`${URL_BASE}/v1/DetalleCompra/compras/${idCompra}/detalles/${idDet}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar el detalle de la compra");
    }

    const data: DetalleCompra = await resp.json();
    return data;
}

// Post Detalle Compra
export const postDetalleCompra = async (token: string | null, detalle: DetalleCompraCreacion): Promise<void | number> => {

    const resp = await fetch(`${URL_BASE}/v1/DetalleCompra`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        },
        body: JSON.stringify(detalle)
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al agregar el detalle de la compra";
        const data = await resp.json();
        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

// Update Detalle Compra
export const updateDetCompra = async (token: string | null, id: string, idCompra: string, detalle: DetalleCompraCreacion)
    : Promise<void | number> => {

    const resp = await fetch(`${URL_BASE}/v1/DetalleCompra/compras/${idCompra}/detalles/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        },
        body: JSON.stringify(detalle)
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        const data = await resp.json();
        let mensajeError = "Error al actualizar el detalle de compra";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

//delete Detalle Compra with Id
export const deleteDetalle = async (token: string | null, idCompra: string, idDet: number): Promise<void | number> => {
    const resp = await fetch(`${URL_BASE}/v1/DetalleCompra/compras/${idCompra}/detalles/${idDet}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        const data = await resp.json();
        let mensajeError = "Error al eliminar el detalle de la compra";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

// Get inventario exist en detalle de compra
export const detalleExistInCompra = async (token: string | null, idInv: number | undefined, compraId: string): Promise<boolean | number> => {

    const resp = await fetch(`${URL_BASE}/v1/DetalleCompra/byInventarioId/${idInv}?compraId=${compraId}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar el detalle de la compra");
    }

    const data = await resp.json();
    return data;

}

// Clientes
// Get Clientes
export const getClientes = async (token: string | null): Promise<Cliente[] | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Clientes`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        throw new Error("Error al cargar los Clientes");
    }

    const data: Cliente[] = await resp.json();
    return data;
}

// Post cliente
export async function postCliente(token: string | null, cliente: ClienteCreacion): Promise<void | number> {
    try {
        const result = await fetch(`${URL_BASE}/v1/Clientes`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${token}`
            },
            body: JSON.stringify(cliente),
        });

        if (result.status === 403) {
            return 403;
        }

        if (!result.ok) {
            let mensajeError = "Error al agregar nuevo Cliente";
            const data = await result.json();
            if (data.errors && Object.keys(data.errors).length > 0) {
                const firstKey = Object.keys(data.errors)[0];
                mensajeError = data.errors[firstKey][0];
            }

            throw new Error(mensajeError);
        }

        return;
    } catch (error: any) {
        const mensaje = error.message || "Error desconocido";
        throw new Error(mensaje);
    }
}

// Get Cliente by id
export const getClienteById = async (token: string | null, id: string): Promise<Cliente | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Clientes/${id}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        let mensajeError = "Error al obtener el Cliente";
        const data = await resp.json();
        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }
    const data: Cliente = await resp.json();

    return data;
}

// Update Cliente
export const updateCliente = async (token: string | null, id: string, cliente: ClienteCreacion)
    : Promise<void | number> => {

    const resp = await fetch(`${URL_BASE}/v1/Clientes/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        },
        body: JSON.stringify(cliente)
    });

    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        const data = await resp.json();
        let mensajeError = "Error al actualizar el cliente";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}

//delete Cliente with Id
export const deleteCliente = async (token: string | null, id: number): Promise<void | number> => {
    const resp = await fetch(`${URL_BASE}/v1/Clientes/${id}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`
        }
    });
    if (resp.status === 403) {
        return 403;
    }
    if (!resp.ok) {
        const data = await resp.json();
        let mensajeError = "Error al eliminar el Cliente";

        if (data.errors && Object.keys(data.errors).length > 0) {
            const firstKey = Object.keys(data.errors)[0];
            mensajeError = data.errors[firstKey][0];
        }

        throw new Error(mensajeError);
    }

    return;
}