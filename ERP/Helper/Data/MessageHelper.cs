namespace ERP.Helper.Data
{
    public class MessageHelper
    {
        public readonly static string errorParamsGeneral = "Parametros incorrectos";

        public readonly static string errorGeneral = "Ocurrio un error, intente más tarde";

        public readonly static string errorDB = "Ocurrio un error con la conexión a la BD";

        public readonly static string profileChangePasswordSuccess = "La contraseña se cambio con éxito";
        public readonly static string profileChangePasswordErrorNotEqualsOldPassword = "La contraseña ingresada no coincide con la anterior";
        public readonly static string profileChangePasswordErrorNotEqualsPass = "Las nuevas contraseñas no son iguales";
        public readonly static string profileChangeNameUserSuccess = "Se cambio el nombre del usuario";
        public readonly static string profileChangeRolUserSuccess = "Se cambio el rol del usuario";

        public readonly static string encryptError = "Ocurrio un error al codificar el texto";
        public readonly static string desencryptError = "Ocurrio un error al decodificar el texto";

        public readonly static string jwtErrorEncrypt = "Ocurrio un error al generar el JWT";
        public readonly static string jwtErrorDesencrypt = "Ocurrio un error al decodificar el token de sesión";
        public readonly static string jwtErrorDesencryptTime = "El token de sesión caducó";


        public readonly static string loginIncorrect = "Usuario y/o contraseña, no existe";
        public readonly static string registerSuccess = "Se creó el usuario con éxtio";
        public readonly static string registerErrorExist = "Ya existe un usuario con el mismo correo";

        public readonly static string pointEmissionCorrect = "Punto de emisión creado correctamente";
        public readonly static string pointEmissionIncorrect = "Error al registrar punto de emisión";
        public readonly static string pointEmissionListCorrect = "Listado de punto de emisión";
        public readonly static string pointEmissionEditCorrect = "Se Editó Punto de emission Correctamente";


        public readonly static string pointSaleCorrect = "Punto de venta creado correctamente";
        public readonly static string pointSaleIncorrect = "Error al crear punto de venta";
        public readonly static string pointSaleListIncorrect = "Error al obtner punto de venta";
        public readonly static string pointSaleListCorrect = "Listado de Punto de venta";
        public readonly static string pointSaleEditCorrect = "Se Editó Punto de Venta Correctamente";

        public readonly static string roleCorrect = "Rol creado correctamente";
        public readonly static string roleIncorrect = "Error al registrar rol";
        public readonly static string roleNotFound = "Rol no encontrado";
        public readonly static string roleDelete = "Rol eliminado correctamente";
        public readonly static string roleErrorDelete = "Error al eliminar rol";
        public readonly static string roleListCorrect = "Listado de Roles";

        public readonly static string PermissionCorrect = "Permiso creado correctamente";
        public readonly static string PermissionIncorrect = "Error al registrar Permiso";
        public readonly static string PermissionNotFound = "Permiso no encontrado";
        public readonly static string PermissionDelete = "Permiso eliminado correctamente";
        public readonly static string PermissionErrorDelete = "Error al eliminar Permiso";
        public readonly static string PermissionListCorrect = "Listado de Permisos";
        public readonly static string PermissionEdit = "Permisos editado correctamente";


        public readonly static string CustomerCorrect = "Cliente creado correctamente";
        public readonly static string CustomerError = "Error al registrar cliente";
        public readonly static string CustomerListCorrect = "Listado de Clientes";
        public readonly static string CustomerNotFound = "Cliente no encontrado";
        public readonly static string CustomerDelete = "Cliente eliminado correctamente";
        public readonly static string CustomerErrorDelete = "Error al eliminar Cliente";
        public readonly static string CustomerEdit = "Se Editó el Cliente Correctamente";

        public readonly static string SupplierCorrect = "Proveedor creado correctamente";
        public readonly static string SupplierError = "Error al registrar Proveedor";
        public readonly static string SupplierListCorrect = "Listado de Proveedor";
        public readonly static string SupplierNotFound = "Proveedor no encontrado";
        public readonly static string SupplierDelete = "Proveedor eliminado correctamente";
        public readonly static string SupplierErrorDelete = "Error al eliminar Proveedor";
        public readonly static string SupplierEdit = "Se Editó el Proveedor Correctamente";


        public readonly static string WarehouseCorrect = "Bodega creado correctamente";
        public readonly static string WarehouseIncorrect = "Error al crear Bodega";
        public readonly static string WarehouseListCorrect = "Listado de Bodegas";
        public readonly static string WarehouseNotFound = "Bodega no encontrado";
        public readonly static string WarehouseDelete = "Bodega eliminado correctamente";
        public readonly static string WarehouseErrorDelete = "Error al eliminar Bodega";
        public readonly static string WarehouseEdit = "Se Editó Bodega Correctamente";


        public readonly static string CompanyCorrect = "Empresa creado correctamente";
        public readonly static string CompanyIncorrect = "Error al crear empresa";
        public readonly static string CompanyListCorrect = "Listado de empresa";
        public readonly static string CompanyNotFound = "Empresa no encontrado";
        public readonly static string CompanyDelete = "empresa eliminado correctamente";
        public readonly static string CompanyErrorDelete = "Error al eliminar empresa";
        public readonly static string CompanyEdit = "Se Editó Empresa Correctamente";

        public readonly static string BranchCorrect = "Sucursal creado correctamente";
        public readonly static string BranchIncorrect = "Error al crear Sucursal";
        public readonly static string BranchListCorrect = "Listado de Sucursal";
        public readonly static string BranchNotFound = "Sucursal no encontrado";
        public readonly static string BranchDelete = "Sucursal eliminado correctamente";
        public readonly static string BranchErrorDelete = "Error al eliminar Sucursal";
        public readonly static string BranchEdit = "Se Editó Sucursal Correctamente";

        public readonly static string MovementCabCorrect = "Movimiento Cabecera creado correctamente";
        public readonly static string MovementCabIncorrect = "Error al crear Movimiento Cabecera";
        public readonly static string MovementCabListCorrect = "Listado de Movimiento Cabecera";
        public readonly static string MovementCabNotFound = "Movimiento Cabecera no encontrado";
        public readonly static string MovementCabEdit = "Se Editó Movimiento Cabecera Correctamente";

        public readonly static string MovementPayCorrect = "Movimiento de Pago creado correctamente";
        public readonly static string MovementPayIncorrect = "Error al crear Movimiento de Pago";
        public readonly static string MovementPayListCorrect = "Listado de Movimiento de Pago";
        public readonly static string MovementPayNotFound = "Movimiento de Pago no encontrado";
        public readonly static string MovementPayEdit = "Se Editó Movimiento de Pago Correctamente";

        public readonly static string MovementDetProductCorrect = "Movimiento de Producto creado correctamente";
        public readonly static string MovementDetProductIncorrect = "Error al crear Movimiento de Producto";
        public readonly static string MovementDetProductListCorrect = "Listado de Movimiento de Producto";
        public readonly static string MovementDetProductNotFound = "Movimiento de Producto no encontrado";
        public readonly static string MovementDetProductEdit = "Se Editó Movimiento de Producto Correctamente";

        public readonly static string CountryCorrect = "Pais creado correctamente";
        public readonly static string CountryIncorrect = "Error al crear País";
        public readonly static string CountryListCorrect = "Listado de País";

        public readonly static string CityCorrect = "Ciudad creado correctamente";
        public readonly static string CityIncorrect = "Error al crear Ciudad";
        public readonly static string CityListCorrect = "Listado de Ciudad";

        public readonly static string FormPayCorrect = "Forma de pago creado correctamente";
        public readonly static string FormPayIncorrect = "Error al crear Forma de pago";
        public readonly static string FormPayListCorrect = "Listado de Forma de pago";
        public readonly static string FormPayNotFound = "Forma de pago no encontrado";
        public readonly static string FormPayEdit = "Se Editó Forma de pago Correctamente";

        public readonly static string CreditCardCorrect = "Tarjeta de Crédito creado correctamente";
        public readonly static string CreditCardIncorrect = "Error al crear Tarjeta de Crédito";
        public readonly static string CreditCardListCorrect = "Listado de Tarjeta de Crédito";
        public readonly static string CreditCardNotFound = "Tarjeta de Crédito no encontrado";
        public readonly static string CreditCardEdit = "Se Editó Tarjeta de Crédito Correctamente";

        public readonly static string ModuleCorrect = "Modulo creado correctamente";
        public readonly static string ModuleIncorrect = "Error al crear Modulo";
        public readonly static string ModuleListCorrect = "Listado de Modulo";
        public readonly static string ModuleNotFound = "Modulo no encontrado";
        public readonly static string ModuleEdit = "Se Editó Modulo Correctamente";

        public readonly static string OptionCorrect = "Opción creado correctamente";
        public readonly static string OptionIncorrect = "Error al crear Opción";
        public readonly static string OptionListCorrect = "Listado de Opción";
        public readonly static string OptionNotFound = "Opción no encontrado";
        public readonly static string OptionEdit = "Se Editó Opción Correctamente";

        public readonly static string BrandCorrect = "Marca creado correctamente";
        public readonly static string BrandIncorrect = "Error al crear Marca";
        public readonly static string BrandListCorrect = "Listado de Marca";
        public readonly static string BrandNotFound = "Marca no encontrado";
        public readonly static string BrandEdit = "Se Editó Marca Correctamente";
        public readonly static string BrandDelete = "Marca eliminado correctamente";
        public readonly static string BrandErrorDelete = "Error al eliminar Marca";

        public readonly static string CategoryCorrect = "Categoría creado correctamente";
        public readonly static string CategoryIncorrect = "Error al crear Categoría";
        public readonly static string CategoryListCorrect = "Listado de Categoría";
        public readonly static string CategoryNotFound = "Categoría no encontrado";
        public readonly static string CategoryEdit = "Se Editó Categoría Correctamente";
        public readonly static string CategoryDelete = "Categoría eliminado correctamente";
        public readonly static string CategoryErrorDelete = "Error al eliminar Categoría";

        public readonly static string IndustryCorrect = "Industria creado correctamente";
        public readonly static string IndustryIncorrect = "Error al crear Industria";
        public readonly static string IndustryListCorrect = "Listado de Industria";
        public readonly static string IndustryNotFound = "Industria no encontrado";
        public readonly static string IndustryEdit = "Se Editó Industria Correctamente";

        public readonly static string ProductCorrect = "Producto creado correctamente";
        public readonly static string ProductIncorrect = "Error al crear Producto";
        public readonly static string ProductListCorrect = "Listado de Producto";
        public readonly static string ProductNotFound = "Producto no encontrado";
        public readonly static string ProductEdit = "Se Editó Producto Correctamente";
        public readonly static string ProductDelete = "Producto eliminado correctamente";
        public readonly static string ProductErrorDelete = "Error al eliminar Producto";

        public readonly static string StockCorrect = "Stock creado correctamente";
        public readonly static string StockIncorrect = "Error al crear Stock";
        public readonly static string StockListCorrect = "Listado de Stock";
        public readonly static string StockNotFound = "Stock no encontrado";
        public readonly static string StockEdit = "Se Editó Stock Correctamente";

        public readonly static string TaxCorrect = "Impuesto creado correctamente";
        public readonly static string TaxIncorrect = "Error al crear Impuesto";
        public readonly static string TaxListCorrect = "Listado de Impuesto";
        public readonly static string TaxNotFound = "Impuesto no encontrado";
        public readonly static string TaxEdit = "Se Editó Impuesto Correctamente";
    }
}