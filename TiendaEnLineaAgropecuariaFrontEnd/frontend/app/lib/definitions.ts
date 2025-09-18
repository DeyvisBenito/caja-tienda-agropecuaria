export type User = {
  Id: number;
  Email: string;
  Password: string;
};

export interface LoginConEmailI {
  Email: string,
  Password: string
};

export interface RecuperarForm {
  email: string;
}

export interface ResetPasswordForm {
  password: string;
  confirmarPassword: string;
}

export interface ReseteoPasswordIn {
  Email: string,
  Token: string,
  NuevoPassword: string
}

export interface ReseteoPasswordResponse {
  message: string,
  success: boolean
}

export interface CredencialesResetearPassword {
  Email: string;
  TokenResetPassword: string;
}


export type Product = {
  Id: number;
  Name: string;
  Price: number;
  Amount: number;
  Description: string;
  message?: string;
};

export type ProductPost = {
  name: string;
  price: number;
  amount: number;
  description: string;
};

export type Token = {
  token: string;
  error?: string;
}

export type ProductsPaginated = {
  Products: Product[];
  Total: number;
}

export type Categoria = {
  id: number,
  nombre: string,
  estado: string,
  estadoId: number
}

export type Inventario = {
  id: number,
  nombre: string,
  tipoProducto?: string,
  tipoProductoId: number,
  estado: string,
  estadoId: number,
  bodega?: string,
  bodegaId: number,
  marca: string,
  precio: number,
  urlFoto: string,
  descripcion: string,
  stock: number
}

export type TipoProducto = {
  id: number,
  nombre: string,
  estado: string,
  estadoId: number,
  categoria: string,
  categoriaId: number
}

export type Bodegas = {
  id: number,
  nombre: string,
  ubicacion: string,
  estado: string,
  estadoId: number
}

export type InventarioCreacion = {
  Nombre : string,
  TipoProductoId : number,
  EstadoId : number,
  BodegaId : number,
  Marca : string,
  Precio : number,
  Descripcion?: string,
  Stock : number,
  Foto?: FileList 
}

export type CarritoSuccess = {
  success: boolean;
}
