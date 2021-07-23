using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class ControladorBD
    {
        String connectionString = "Data Source=sqlserverdistribuidorafyj.database.windows.net; Initial Catalog=dbdistribuidorafyj;User Id=estivenalvarez;Password=estiven.1234";


        //APARTADO DE USUARIO

        /// <summary>
        /// Este método se encarga de acceder a una conexión, crear un comando que a través de un nombre, 
        /// obtenga un usuario objeto.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns>El usuario si fue encontrado, de lo contrario valor nulo.</returns>
        public Usuario obtenerUsuarioNombre(string nombre)
        {
            SqlConnection connection = new SqlConnection(@connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT nombreUsuario, contrasena, FK_bodegaID, FK_personaID, FK_rolID, activoSN FROM Persona.Usuario WHERE nombreUsuario = @nombre;", connection);


                command.Parameters.Add("@nombre", SqlDbType.NVarChar, 50);
                command.Parameters["@nombre"].Value = nombre;

                Usuario usuarioTemp = new Usuario();
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        usuarioTemp.nombreUsuario = reader.GetString(0);
                        usuarioTemp.contrasena = reader.GetString(1);
                        usuarioTemp.FK_bodegaID = reader.GetInt32(2);
                        usuarioTemp.FK_personaID = reader.GetInt32(3);
                        usuarioTemp.FK_rolID = reader.GetInt32(4);
                        usuarioTemp.activoSN = reader.GetBoolean(5);
                    }
                }
                else
                {
                    return null;
                }
                reader.Close();
                return usuarioTemp;
            }
        }

        /// <summary>
        /// Este método tiene la función de obtener un objeto persona desde un identificador obtenido por cada usuario.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un objeto persona.</returns>
        public Persona obtenerPersona(int id)
        {
            SqlConnection connection = new SqlConnection(@connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand(
                  "SELECT personaID, nombre FROM Persona.Persona WHERE personaID = @id;",
                  connection);

                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;

                Persona personaTemp = new Persona();
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        personaTemp.personaID = reader.GetInt32(0);
                        personaTemp.nombre = reader.GetString(1);
                    }
                }
                else
                {
                    return null;
                }
                reader.Close();
                return personaTemp;
            }
        }

        /// <summary>
        /// Este método recibe los datos verificados y necesarios para ingresar un usuario nuevo en la 
        /// base de datos a través de un "query"
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <param name="identificador"></param>
        /// <param name="contrasena"></param>
        /// <param name="fkBodega"></param>
        /// <param name="fkRol"></param>
        public void agregarUsuario(String nombreUsuario, String identificador, String contrasena, int fkBodega, int fkRol)
        {
            SqlConnection connection = new SqlConnection(@connectionString);

            string sqlInsert = "INSERT INTO Persona.Usuario(nombreUsuario, contrasena, FK_bodegaID, FK_personaID, FK_rolID, activoSN) VALUES (@nombre , @contrasena, @fkBodega, @fkPersona, @fkRol, @activoSN)";

            SqlCommand insertCommand = new SqlCommand(sqlInsert, connection);

            insertCommand.Parameters.Add("@nombre", SqlDbType.NVarChar, 50);
            insertCommand.Parameters["@nombre"].Value = identificador;

            insertCommand.Parameters.Add("@contrasena", SqlDbType.NVarChar, 50);
            insertCommand.Parameters["@contrasena"].Value = contrasena;

            insertCommand.Parameters.Add("@fkBodega", SqlDbType.Int);
            insertCommand.Parameters["@fkBodega"].Value = fkBodega;

            insertCommand.Parameters.Add("@fkRol", SqlDbType.Int);
            insertCommand.Parameters["@fkRol"].Value = fkRol;

            insertCommand.Parameters.Add("@activoSN", SqlDbType.Bit);
            insertCommand.Parameters["@activoSN"].Value = true;

            insertCommand.Parameters.Add("@fkPersona", SqlDbType.Int);
            insertCommand.Parameters["@fkPersona"].Value = agregarPersona(nombreUsuario);

            connection.Open();
            insertCommand.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Al ser todo usuario una persona, este método viene a recibir el dato necesario para crear 
        /// una persona nueva a través de un query. 
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <returns>El ID de la persona recién creada si tuvo éxito, de lo contrario un 0.</returns>
        private int agregarPersona(String nombreUsuario)
        {
            SqlConnection connection = new SqlConnection(@connectionString);
            int idPersona = 0;
            string sqlInsert = "INSERT INTO Persona.Persona(nombre) VALUES (@nombre);";
            string sqlReader = "SELECT MAX(personaID) FROM Persona.Persona;";

            SqlCommand insertCommand = new SqlCommand(sqlInsert, connection);
            SqlCommand readerCommand = new SqlCommand(sqlReader, connection);

            insertCommand.Parameters.Add("@nombre", SqlDbType.NVarChar, 50);
            insertCommand.Parameters["@nombre"].Value = nombreUsuario;

            connection.Open();
            insertCommand.ExecuteNonQuery();
            SqlDataReader reader = readerCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    idPersona = reader.GetInt32(0);
                }
                return idPersona;
            }
            connection.Close();
            reader.Close();
            return 0;
        }

        /// <summary>
        /// Este método se encarga de recorrer la base a través de un "query", valida que solo sean
        /// los que estén activos y crea una lista de usuarios en la que se van agregando cada uno de 
        /// los devueltos por la consulta.
        /// </summary>
        /// <returns>Una lista completa de usuarios.</returns>
        public List<Usuario> buscarUsuarios()
        {
            SqlConnection connection = new SqlConnection(@connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand(
                  "SELECT nombreUsuario, contrasena, FK_bodegaID, FK_personaID, FK_rolID, activoSN FROM Persona.Usuario;",
                  connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                List<Usuario> listaUsuarios = new List<Usuario>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Usuario usuarioTemp = new Usuario();
                        usuarioTemp.nombreUsuario = reader.GetString(0);
                        usuarioTemp.contrasena = reader.GetString(1);
                        usuarioTemp.FK_bodegaID = reader.GetInt32(2);
                        usuarioTemp.FK_personaID = reader.GetInt32(3);
                        usuarioTemp.FK_rolID = reader.GetInt32(4);
                        usuarioTemp.activoSN = reader.GetBoolean(5);
                        listaUsuarios.Add(usuarioTemp);
                    }
                }
                else
                {
                    return listaUsuarios;
                }
                reader.Close();
                return listaUsuarios;
            }

        }

        /// <summary>
        /// Método que recibe los valores validados y necesarios para actualizar los datos solicitados por el administrador.
        /// Lo busca a través del ID y una vez obtenido actualiza sus datos. 
        /// </summary>
        /// <param name="personaID"></param>
        /// <param name="nombreUsuario"></param>
        /// <param name="identificador"></param>
        /// <param name="contrasena"></param>
        /// <param name="fkBodega"></param>
        /// <returns>true si se pudo sin problema, de lo contrario false </returns>
        public Boolean modificarUsuario(int personaID, String nombreUsuario, String identificador, String contrasena, int fkBodega)
        {
            try
            {
                SqlConnection connection = new SqlConnection(@connectionString);
                SqlCommand command = new SqlCommand(
                      "UPDATE Persona.Persona SET nombre = @nombre WHERE personaID = @fkPersonaid;",
                      connection);
                SqlCommand commandUsuario = new SqlCommand(
                      "UPDATE Persona.Usuario SET contrasena = @contrasena, FK_bodegaID = @fkBodega WHERE nombreUsuario = @identificador;",
                      connection);

                command.Parameters.Add("@fkPersonaid", SqlDbType.Int);
                command.Parameters["@fkPersonaid"].Value = personaID;

                command.Parameters.Add("@nombre", SqlDbType.NVarChar, 50);
                command.Parameters["@nombre"].Value = nombreUsuario;

                commandUsuario.Parameters.Add("@contrasena", SqlDbType.NVarChar, 50);
                commandUsuario.Parameters["@contrasena"].Value = contrasena;

                commandUsuario.Parameters.Add("@fkBodega", SqlDbType.Int);
                commandUsuario.Parameters["@fkBodega"].Value = fkBodega;

                commandUsuario.Parameters.Add("@identificador", SqlDbType.NVarChar, 50);
                commandUsuario.Parameters["@identificador"].Value = identificador;

                connection.Open();
                command.ExecuteNonQuery();
                commandUsuario.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Método que se encarga de hacer un borrado lógico en la base de datos, buscandolo a través
        /// del identificador y que de esta forma desaparezca del sistema. 
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <returns>true si se pudo sin problema, de lo contrario false</returns>
        public Boolean eliminarUsuario(string nombreUsuario)
        {
            try
            {
                SqlConnection connection = new SqlConnection(@connectionString);
                SqlCommand command = new SqlCommand(
                      "UPDATE Persona.Usuario SET activoSN = @activoSN WHERE nombreUsuario = @nombreUsuario",
                      connection);

                command.Parameters.Add("@nombreUsuario", SqlDbType.NVarChar, 50);
                command.Parameters["@nombreUsuario"].Value = nombreUsuario;

                command.Parameters.Add("@activoSN", SqlDbType.Bit);
                command.Parameters["@activoSN"].Value = false;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }





        //APARTADO DE BODEGA

        /// <summary>
        /// Método encargado de agregar a la base de datos un objeto bodega con todos los datos que vienen desde la capa logica de negocios.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="ubicacion"></param>
        public void agregarBodega(String nombre, String ubicacion)
        {
            SqlConnection connection = new SqlConnection(@connectionString);

            string sqlInsert = "INSERT INTO Inventario.Bodega(nombre, ubicacion, activoSN) VALUES (@nombre , @ubicacion, @activoSN)";

            SqlCommand insertCommand = new SqlCommand(sqlInsert, connection);

            insertCommand.Parameters.Add("@nombre", SqlDbType.NVarChar, 50);
            insertCommand.Parameters["@nombre"].Value = nombre;

            insertCommand.Parameters.Add("@ubicacion", SqlDbType.NVarChar, 300);
            insertCommand.Parameters["@ubicacion"].Value = ubicacion;

            insertCommand.Parameters.Add("@activoSN", SqlDbType.Bit);
            insertCommand.Parameters["@activoSN"].Value = true;

            connection.Open();
            insertCommand.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Este método se encarga de recorrer la base a través de un "query", valida que solo sean
        /// las bodegas que estén activas y crea una lista de bodegas en la que se van agregando cada uno de 
        /// los devueltos por la consulta.
        /// </summary>
        /// <returns>Retorna una lista con todas las bodegas que se leyeron de la base de datos.</returns>
        public List<Bodega> buscarBodegas()
        {
            SqlConnection connection = new SqlConnection(@connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand(
                  "SELECT bodegaID, nombre, ubicacion, activoSN FROM Inventario.Bodega;",
                  connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                List<Bodega> listaBodegas = new List<Bodega>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Bodega bodegaTemp = new Bodega();
                        bodegaTemp.bodegaID = reader.GetInt32(0);
                        bodegaTemp.nombre = reader.GetString(1);
                        bodegaTemp.ubicacion = reader.GetString(2);
                        bodegaTemp.activoSN = reader.GetBoolean(3);
                        listaBodegas.Add(bodegaTemp);
                    }
                }
                else
                {
                    return listaBodegas;
                }
                reader.Close();
                return listaBodegas;
            }

        }

        /// <summary>
        /// Método encargado de modificar una bodega en específico con los datos que se enviaron desde la capa de negocios.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="ubicacion"></param>
        /// <returns>Retorna un booleano true confirmando la modificación de la bodega, de lo contrario retorna un false.</returns>
        public Boolean modificarBodega(int id, String nombre, String ubicacion)
        {
            try
            {
                SqlConnection connection = new SqlConnection(@connectionString);
                SqlCommand command = new SqlCommand(
                      "UPDATE Inventario.Bodega SET nombre = @nombre, ubicacion = @ubicacion WHERE bodegaID = @id;",
                      connection);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;
                command.Parameters.Add("@nombre", SqlDbType.NVarChar, 50);
                command.Parameters["@nombre"].Value = nombre;
                command.Parameters.Add("@ubicacion", SqlDbType.NVarChar, 300);
                command.Parameters["@ubicacion"].Value = ubicacion;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Método que se encarga de hacer un borrado lógico en la base de datos, buscandolo a través
        /// del identificador y que de esta forma desaparezca del sistema. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano true si se pudo eliminar, de lo contrario se retorna false.</returns>
        public Boolean eliminarBodega(int id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(@connectionString);
                SqlCommand command = new SqlCommand(
                      "UPDATE Inventario.Bodega SET activoSN = @activoSN WHERE bodegaID = @id",
                      connection);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;

                command.Parameters.Add("@activoSN", SqlDbType.Bit);
                command.Parameters["@activoSN"].Value = false;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }



        //APARTADO DE RUTAS

        /// <summary>
        /// Método encargado de agregar a la base de datos un objeto ruta con todos los datos que vienen desde la capa logica de negocios.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="fkUsuario"></param>
        public void agregarRuta(String nombre, String fkUsuario)
        {
            SqlConnection connection = new SqlConnection(@connectionString);

            string sqlInsert = "INSERT INTO Inventario.Ruta(nombre, FK_nombreUsuario, activoSN) VALUES (@nombre , @fkUsuario, @activoSN)";

            SqlCommand insertCommand = new SqlCommand(sqlInsert, connection);

            insertCommand.Parameters.Add("@nombre", SqlDbType.NVarChar, 50);
            insertCommand.Parameters["@nombre"].Value = nombre;

            insertCommand.Parameters.Add("@fkUsuario", SqlDbType.NVarChar, 50);
            insertCommand.Parameters["@fkUsuario"].Value = fkUsuario;

            insertCommand.Parameters.Add("@activoSN", SqlDbType.Bit);
            insertCommand.Parameters["@activoSN"].Value = true;

            connection.Open();
            insertCommand.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Este método se encarga de recorrer la base a través de un "query", valida que solo sean
        /// las rutas que estén activas y crea una lista de rutas en la que se van agregando cada uno de 
        /// los devueltos por la consulta.
        /// </summary>
        /// <returns>Retorna una lista con las rutas que se leyeron de la base de datos.</returns>
        public List<Ruta> buscarRutas()
        {
            SqlConnection connection = new SqlConnection(@connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand(
                  "SELECT rutaID, nombre, FK_nombreUsuario, activoSN FROM Inventario.Ruta;",
                  connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                List<Ruta> listaRutas = new List<Ruta>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Ruta rutaTemp = new Ruta();
                        rutaTemp.rutaID = reader.GetInt32(0);
                        rutaTemp.nombre = reader.GetString(1);
                        rutaTemp.FK_nombreUsuario = reader.GetString(2);
                        rutaTemp.activoSN = reader.GetBoolean(3);
                        listaRutas.Add(rutaTemp);
                    }
                }
                else
                {
                    return listaRutas;
                }
                reader.Close();
                return listaRutas;
            }

        }

        /// <summary>
        /// Método que recibe los valores validados y necesarios para actualizar los datos solicitados por el administrador.
        /// Lo busca a través del ID y una vez obtenido actualiza sus datos. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="fkUsuario"></param>
        /// <returns>Retorna un valor booleano true si se pudo modificar con éxito, de lo contrario un false.</returns>
        public Boolean modificarRuta(int id, String nombre, String fkUsuario)
        {
            try
            {
                SqlConnection connection = new SqlConnection(@connectionString);
                SqlCommand command = new SqlCommand(
                      "UPDATE Inventario.Ruta SET nombre = @nombre, FK_nombreUsuario = @fkUsuario WHERE rutaID = @id;",
                      connection);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;
                command.Parameters.Add("@nombre", SqlDbType.NVarChar, 50);
                command.Parameters["@nombre"].Value = nombre;
                command.Parameters.Add("@fkUsuario", SqlDbType.NVarChar, 300);
                command.Parameters["@fkUsuario"].Value = fkUsuario;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Método que se encarga de hacer un borrado lógico en la base de datos, buscandolo a través
        /// del identificador y que de esta forma desaparezca del sistema.  
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un valor booleano true confirmando el borrado con éxito, de lo contrario un false.</returns>
        public Boolean eliminarRuta(int id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(@connectionString);
                SqlCommand command = new SqlCommand(
                      "UPDATE Inventario.Ruta SET activoSN = @activoSN WHERE rutaID = @id",
                      connection);

                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;

                command.Parameters.Add("@activoSN", SqlDbType.Bit);
                command.Parameters["@activoSN"].Value = false;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        //APARTADO DE PRODUCTOS

        /// <summary>
        /// Método encargado de agregar a la base de datos un objeto producto con todos los datos que vienen desde la capa logica de negocios.
        /// Además de llamar al método designarBodegaXProducto para asignar una cantidad de productos a la bodega deseada.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="tipo"></param>
        /// <param name="precioCosto"></param>
        /// <param name="precioCostoAgente"></param>
        /// <param name="precioVenta"></param>
        /// <param name="precioBaseVenta"></param>
        /// <param name="cantidad"></param>
        public void agregarProducto(string nombre, string descripcion, string tipo, decimal precioCosto, decimal precioCostoAgente, decimal precioVenta, decimal precioBaseVenta, int cantidad)
        {
            SqlConnection connection = new SqlConnection(@connectionString);
            SqlConnection connection2 = new SqlConnection(@connectionString);
            int idNuevoProducto = 0;

            string sqlInsert = "INSERT INTO Inventario.Producto(nombre, descripcion, tipo, precioCosto, precioCostoAgente, precioVenta, precioBaseVenta, activoSN) " +
                                                                    "VALUES (@nombre, @descripcion, @tipo, @precioCosto, @precioCostoAgente, @precioVenta, @precioBaseVenta, @activoSN)";

            string sqlReader = "SELECT MAX(productoID) FROM Inventario.Producto;";


            SqlCommand insertCommand = new SqlCommand(sqlInsert, connection);

            SqlCommand readerCommand = new SqlCommand(sqlReader, connection2);

            insertCommand.Parameters.Add("@nombre", SqlDbType.NVarChar, 50);
            insertCommand.Parameters["@nombre"].Value = nombre;

            insertCommand.Parameters.Add("@descripcion", SqlDbType.NVarChar, 250);
            insertCommand.Parameters["@descripcion"].Value = descripcion;

            insertCommand.Parameters.Add("@tipo", SqlDbType.NVarChar, 50);
            insertCommand.Parameters["@tipo"].Value = tipo;

            insertCommand.Parameters.Add("@precioCosto", SqlDbType.Money);
            insertCommand.Parameters["@precioCosto"].Value = precioCosto;

            insertCommand.Parameters.Add("@precioCostoAgente", SqlDbType.Money);
            insertCommand.Parameters["@precioCostoAgente"].Value = precioCostoAgente;

            insertCommand.Parameters.Add("@precioVenta", SqlDbType.Money);
            insertCommand.Parameters["@precioVenta"].Value = precioVenta;

            insertCommand.Parameters.Add("@precioBaseVenta", SqlDbType.Money);
            insertCommand.Parameters["@precioBaseVenta"].Value = precioBaseVenta;

            insertCommand.Parameters.Add("@activoSN", SqlDbType.Bit);
            insertCommand.Parameters["@activoSN"].Value = true;

            connection.Open();
            insertCommand.ExecuteNonQuery();
            connection.Close();

            connection2.Open();
            SqlDataReader reader = readerCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    idNuevoProducto = reader.GetInt32(0);
                }
            }
            connection2.Close();
            designarBodegaXProducto(idNuevoProducto, cantidad);

        }

        /// <summary>
        /// Método encargado de agregar cierta cantidad de productos a una bodega en específico.
        /// </summary>
        /// <param name="productoID"></param>
        /// <param name="cantidad"></param>
        private void designarBodegaXProducto(int productoID, int cantidad)
        {
            SqlConnection connection = new SqlConnection(@connectionString);
            string sqlInsertBxP = "INSERT INTO Inventario.BodegaProducto(fechaIngresoBodega, cantidad, FK_productoID, FK_bodegaID) VALUES (GETDATE(), @cantidad, @fkProducto, @fkBodega)";
            SqlCommand insertCommandBxP = new SqlCommand(sqlInsertBxP, connection);


            insertCommandBxP.Parameters.Add("@cantidad", SqlDbType.Int);
            insertCommandBxP.Parameters["@cantidad"].Value = cantidad;

            insertCommandBxP.Parameters.Add("@fkProducto", SqlDbType.Int);
            insertCommandBxP.Parameters["@fkProducto"].Value = productoID;

            insertCommandBxP.Parameters.Add("@fkBodega", SqlDbType.Int);
            insertCommandBxP.Parameters["@fkBodega"].Value = 3;
            connection.Open();
            insertCommandBxP.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Método encargado de leer la cantidad de un producto en específico en una bodega en específico.
        /// </summary>
        /// <param name="fkBodega"></param>
        /// <param name="fkProducto"></param>
        /// <returns>Retorna un valor int con la cantidad de productos que se leyeron de la base de datos.</returns>
        public int obtenerCantidadProductoEsp(int fkBodega, int fkProducto)
        {
            int cantidadDicponible = 0;
            SqlConnection connection = new SqlConnection(@connectionString);
            string sqlSelect = "SELECT cantidad FROM Inventario.BodegaProducto WHERE FK_productoID = @FKproducto AND FK_bodegaID = @FKbodega";
            SqlCommand readerCommand = new SqlCommand(sqlSelect, connection);

            readerCommand.Parameters.Add("@FKproducto", SqlDbType.Int);
            readerCommand.Parameters["@FKproducto"].Value = fkProducto;

            readerCommand.Parameters.Add("@FKbodega", SqlDbType.Int);
            readerCommand.Parameters["@FKbodega"].Value = fkBodega;

            connection.Open();
            SqlDataReader reader = readerCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cantidadDicponible = reader.GetInt32(0);
                }
            }
            connection.Close();
            return cantidadDicponible;
        }

        /// <summary>
        /// Método encargado de pasar una cantidad de producto específico de una bodega a otra, realizando un update en la base de datos.
        /// </summary>
        /// <param name="fkBodegaOrigen"></param>
        /// <param name="fkBodegaDestino"></param>
        /// <param name="fkProducto"></param>
        /// <param name="cantidadDespachada"></param>
        /// <param name="cantidadActualizada"></param>
        /// <returns>Retorna un valor booleano true confirmando el despachado con éxito, de lo contrario retorna un false.</returns>
        public Boolean despacharProducto(int fkBodegaOrigen, int fkBodegaDestino, int fkProducto, int cantidadDespachada, int cantidadActualizada)
        {
            SqlConnection connection = new SqlConnection(@connectionString);
            string sqlUpdate = "UPDATE Inventario.BodegaProducto SET cantidad = @nuevaCantidad WHERE FK_productoID = @FKproducto AND FK_bodegaID = @FKbodegaOrigen";
            string sqlInsertBxP = "INSERT INTO Inventario.BodegaProducto(fechaIngresoBodega, cantidad, FK_productoID, FK_bodegaID) VALUES (GETDATE(), @cantidad, @FKproducto , @FKbodegaD)";
            SqlCommand updateCommand = new SqlCommand(sqlUpdate, connection);
            SqlCommand insertCommand = new SqlCommand(sqlInsertBxP, connection);

            updateCommand.Parameters.Add("@nuevaCantidad", SqlDbType.Int);
            updateCommand.Parameters["@nuevaCantidad"].Value = cantidadActualizada;

            updateCommand.Parameters.Add("@FKproducto", SqlDbType.Int);
            updateCommand.Parameters["@FKproducto"].Value = fkProducto;

            updateCommand.Parameters.Add("@FKbodegaOrigen", SqlDbType.Int);
            updateCommand.Parameters["@FKbodegaOrigen"].Value = fkBodegaOrigen;

            insertCommand.Parameters.Add("@cantidad", SqlDbType.Int);
            insertCommand.Parameters["@cantidad"].Value = cantidadDespachada;

            insertCommand.Parameters.Add("@FKproducto", SqlDbType.Int);
            insertCommand.Parameters["@FKproducto"].Value = fkProducto;

            insertCommand.Parameters.Add("@FKbodegaD", SqlDbType.Int);
            insertCommand.Parameters["@FKbodegaD"].Value = fkBodegaDestino;

            try
            {
                connection.Open();
                updateCommand.ExecuteNonQuery();
                insertCommand.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Método que recibe los valores validados y necesarios para actualizar los datos solicitados por el administrador.
        /// Lo busca a través del ID y una vez obtenido actualiza sus datos.  
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="tipo"></param>
        /// <param name="precioCosto"></param>
        /// <param name="precioCostoAgente"></param>
        /// <param name="precioVenta"></param>
        /// <param name="precioBaseVenta"></param>
        /// <param name="cantidad"></param>
        /// <returns>Retorna un valor booleano true si se pudo modificar con éxito, de lo contario retorna un false.</returns>
        public bool modificarProducto(int id, string nombre, string descripcion, string tipo, decimal precioCosto, decimal precioCostoAgente, decimal precioVenta, decimal precioBaseVenta, int cantidad)
        {
            try
            {
                SqlConnection connection = new SqlConnection(@connectionString);
                SqlCommand command = new SqlCommand(
                      "UPDATE Inventario.Producto SET nombre = @nombre, tipo = @tipo, descripcion = @descripcion, precioCosto = @precioCosto, precioCostoAgente = @precioCostoAgente, precioVenta = @precioVenta, " +
                      "precioBaseVenta = @precioBaseVenta WHERE productoID = @id;", connection);
                SqlCommand commandBxP = new SqlCommand(
                      "UPDATE Inventario.BodegaProducto SET cantidad = @cantidad WHERE FK_productoID = @id;", connection);

                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;

                command.Parameters.Add("@nombre", SqlDbType.NVarChar, 50);
                command.Parameters["@nombre"].Value = nombre;

                command.Parameters.Add("@descripcion", SqlDbType.NVarChar, 250);
                command.Parameters["@descripcion"].Value = descripcion;

                command.Parameters.Add("@tipo", SqlDbType.NVarChar, 50);
                command.Parameters["@tipo"].Value = tipo;

                command.Parameters.Add("@precioCosto", SqlDbType.Money);
                command.Parameters["@precioCosto"].Value = precioCosto;

                command.Parameters.Add("@precioCostoAgente", SqlDbType.Money);
                command.Parameters["@precioCostoAgente"].Value = precioCostoAgente;

                command.Parameters.Add("@precioVenta", SqlDbType.Money);
                command.Parameters["@precioVenta"].Value = precioVenta;

                command.Parameters.Add("@precioBaseVenta", SqlDbType.Money);
                command.Parameters["@precioBaseVenta"].Value = precioBaseVenta;

                commandBxP.Parameters.Add("@id", SqlDbType.Int);
                commandBxP.Parameters["@id"].Value = id;

                commandBxP.Parameters.Add("@cantidad", SqlDbType.Int);
                commandBxP.Parameters["@cantidad"].Value = cantidad;

                connection.Open();
                command.ExecuteNonQuery();
                commandBxP.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Método que se encarga de hacer un borrado lógico en la base de datos, buscandolo a través
        /// del identificador y que de esta forma desaparezca del sistema.  
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un valor booleano true confirmando el borrado con éxito, de lo contrario un false.</returns>
        public bool eliminarProducto(int id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(@connectionString);
                SqlCommand command = new SqlCommand(
                      "UPDATE Inventario.Producto SET activoSN = @activoSN WHERE productoID = @id",
                      connection);


                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;

                command.Parameters.Add("@activoSN", SqlDbType.Bit);
                command.Parameters["@activoSN"].Value = false;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Este método se encarga de recorrer la base a través de un "query", valida que solo sean
        /// los productos que estén activos y crea una lista de productos en la que se van agregando cada uno de 
        /// los devueltos por la consulta.
        /// </summary>
        /// <returns>Retorna una lista de los productos que se leyeron desde la base de datos.</returns>
        public List<Producto> buscarProductos()
        {
            SqlConnection connection = new SqlConnection(@connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand(
                  "SELECT productoID, nombre, descripcion, tipo, precioCosto, precioCostoAgente, precioVenta, precioBaseVenta, activoSN FROM Inventario.Producto;",
                  connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                List<Producto> listaProductos = new List<Producto>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Producto productoTemp = new Producto();
                        productoTemp.productoID = reader.GetInt32(0);
                        productoTemp.nombre = reader.GetString(1);
                        productoTemp.descripcion = reader.GetString(2);
                        productoTemp.tipo = reader.GetString(3);
                        productoTemp.precioCosto = reader.GetDecimal(4);
                        productoTemp.precioCostoAgente = reader.GetDecimal(5);
                        productoTemp.precioVenta = reader.GetDecimal(6);
                        productoTemp.precioBaseVenta = reader.GetDecimal(7);
                        productoTemp.activoSN = reader.GetBoolean(8);

                        listaProductos.Add(productoTemp);
                    }
                }
                else
                {
                    return listaProductos;
                }
                reader.Close();
                return listaProductos;
            }
        }

        /// <summary>
        /// Este método se encarga de recorrer la base a través de un "query", valida que solo sean
        /// los productos que estén activos y crea una lista de bodegaXproducto en la que se van agregando cada uno de 
        /// los devueltos por la consulta.
        /// </summary>
        /// <returns>Retorna una lista de los bodegaXproducto que se leyeron desde la base de datos.</returns>
        public List<BodegaProducto> buscarBodegaXProducto()
        {
            SqlConnection connection = new SqlConnection(@connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand(
                  "SELECT cantidad, FK_productoID, FK_bodegaID FROM Inventario.BodegaProducto;",
                  connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                List<BodegaProducto> listaBodegaXProducto = new List<BodegaProducto>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        BodegaProducto bodegaProductoTemp = new BodegaProducto();
                        bodegaProductoTemp.cantidad = reader.GetInt32(0);
                        bodegaProductoTemp.FK_productoID = reader.GetInt32(1);
                        bodegaProductoTemp.FK_bodegaID = reader.GetInt32(2);

                        listaBodegaXProducto.Add(bodegaProductoTemp);
                    }
                }
                else
                {
                    return listaBodegaXProducto;
                }
                reader.Close();
                return listaBodegaXProducto;
            }
        }


        //APARTADO DE CLIENTES

        /// <summary>
        /// Método encargado de agregar a la base de datos un objeto cliente con todos los datos que vienen desde la capa logica de negocios.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="contacto"></param>
        /// <param name="fkRuta"></param>
        public void agregarCliente(string nombre, string contacto, int fkRuta)
        {
            SqlConnection connection = new SqlConnection(@connectionString);

            string sqlInsert = "INSERT INTO Persona.Cliente(contacto, FK_rutaID, FK_personaID, activoSN) VALUES (@contacto, @fkRuta, @fkPersona, @activoSN)";

            SqlCommand insertCommand = new SqlCommand(sqlInsert, connection);

            insertCommand.Parameters.Add("@contacto", SqlDbType.NVarChar, 50);
            insertCommand.Parameters["@contacto"].Value = contacto;

            insertCommand.Parameters.Add("@fkRuta", SqlDbType.Int);
            insertCommand.Parameters["@fkRuta"].Value = fkRuta;

            insertCommand.Parameters.Add("@fkPersona", SqlDbType.Int);
            insertCommand.Parameters["@fkPersona"].Value = agregarPersona(nombre);

            insertCommand.Parameters.Add("@activoSN", SqlDbType.Bit);
            insertCommand.Parameters["@activoSN"].Value = true;

            connection.Open();
            insertCommand.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Este método se encarga de recorrer la base a través de un "query", valida que solo sean
        /// los clientes que estén activos y crea una lista de clientes en la que se van agregando cada uno de 
        /// los devueltos por la consulta.
        /// </summary>
        /// <returns>Retorna una lista con todos los clientes leídos de la base de datos.</returns>
        public List<Cliente> buscarClientes()
        {
            SqlConnection connection = new SqlConnection(@connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand(
                "SELECT clienteID, contacto, FK_rutaID, FK_personaID, activoSN FROM Persona.Cliente;", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                List<Cliente> listaClientes = new List<Cliente>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Cliente clienteTemp = new Cliente();
                        clienteTemp.clienteID = reader.GetInt32(0);
                        clienteTemp.contacto = reader.GetString(1);
                        clienteTemp.FK_rutaID = reader.GetInt32(2);
                        clienteTemp.FK_personaID = reader.GetInt32(3);
                        clienteTemp.activoSN = reader.GetBoolean(4);
                        listaClientes.Add(clienteTemp);
                    }
                }
                else
                {
                    return listaClientes;
                }
                reader.Close();
                return listaClientes;
            }
        }

        /// <summary>
        /// Método que recibe los valores validados y necesarios para actualizar los datos solicitados por el administrador.
        /// Lo busca a través del ID y una vez obtenido actualiza sus datos.  
        /// </summary>
        /// <param name="personaID"></param>
        /// <param name="clienteID"></param>
        /// <param name="contacto"></param>
        /// <param name="nombre"></param>
        /// <param name="fkRuta"></param>
        /// <returns>Retorna un valor booleano true si se pudo modificar con éxito, de lo contrario un false.</returns>
        public Boolean modificarCliente(int personaID, int clienteID, string contacto, string nombre, int fkRuta)
        {
            try
            {
                SqlConnection connection = new SqlConnection(@connectionString);
                SqlCommand command = new SqlCommand(
                      "UPDATE Persona.Persona SET nombre = @nombre WHERE personaID = @fkPersonaid;",
                      connection);
                SqlCommand commandCliente = new SqlCommand(
                      "UPDATE Persona.Cliente SET contacto = @contacto, FK_rutaID = @fkRutaID  WHERE clienteID = @clienteID;",
                      connection);

                command.Parameters.Add("@fkPersonaid", SqlDbType.Int);
                command.Parameters["@fkPersonaid"].Value = personaID;

                command.Parameters.Add("@nombre", SqlDbType.NVarChar, 50);
                command.Parameters["@nombre"].Value = nombre;

                commandCliente.Parameters.Add("@clienteID", SqlDbType.Int);
                commandCliente.Parameters["@clienteID"].Value = clienteID;

                commandCliente.Parameters.Add("@contacto", SqlDbType.NVarChar, 50);
                commandCliente.Parameters["@contacto"].Value = contacto;

                commandCliente.Parameters.Add("@fkRutaID", SqlDbType.Int);
                commandCliente.Parameters["@fkRutaID"].Value = fkRuta;

                connection.Open();
                command.ExecuteNonQuery();
                commandCliente.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Método que se encarga de hacer un borrado lógico en la base de datos, buscandolo a través
        /// del identificador y que de esta forma desaparezca del sistema. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un valor booleano true confirmando el borrado con éxito, de lo contrario un false.</returns>
        public Boolean eliminarCliente(int id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(@connectionString);
                SqlCommand command = new SqlCommand(
                      "UPDATE Persona.Cliente SET activoSN = @activoSN WHERE clienteID = @id",
                      connection);

                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;

                command.Parameters.Add("@activoSN", SqlDbType.Bit);
                command.Parameters["@activoSN"].Value = false;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }



        //APARTADO DE CUENTA

        /// <summary>
        /// Método encargado de agregar a la base de datos un objeto cuenta con todos los datos que vienen desde la capa logica de negocios.
        /// </summary>
        /// <param name="clienteID"></param>
        /// <param name="tipoCuenta"></param>
        /// <param name="fkUsuario"></param>
        /// <returns></returns>
        public Boolean guardarCuenta(int clienteID, string tipoCuenta, string fkUsuario)
        {
            try
            {
                SqlConnection connection = new SqlConnection(@connectionString);

                string sqlInsert = "INSERT INTO Facturacion.Cuenta(tipo, fechaCreacion, FK_clienteID, FK_nombreUsuario) VALUES (@tipo, @fechaCreacion, @fkCliente, @fkUsuario)";

                SqlCommand insertCommand = new SqlCommand(sqlInsert, connection);

                insertCommand.Parameters.Add("@tipo", SqlDbType.NVarChar, 30);
                insertCommand.Parameters["@tipo"].Value = tipoCuenta;

                insertCommand.Parameters.Add("@fechaCreacion", SqlDbType.DateTime);
                insertCommand.Parameters["@fechaCreacion"].Value = DateTime.Now;

                insertCommand.Parameters.Add("@fkCliente", SqlDbType.Int);
                insertCommand.Parameters["@fkCliente"].Value = clienteID;

                insertCommand.Parameters.Add("@fkUsuario", SqlDbType.NVarChar, 30);
                insertCommand.Parameters["@fkUsuario"].Value = fkUsuario;

                connection.Open();
                insertCommand.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }


        /// <summary>
        /// Este método se encarga de recorrer la base a través de un "query", crea una lista de clientes en la que se van agregando cada uno de 
        /// las cuentas devueltas por la consulta.
        /// </summary>
        /// <returns>Retorna una lista de cuentas</returns>
        public List<Cuenta> obtenerCuentas()
        {
            SqlConnection connection = new SqlConnection(@connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand(
                  "SELECT cuentaID, tipo, fechaCreacion, FK_clienteID, FK_nombreUsuario FROM Facturacion.Cuenta;",
                  connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                List<Cuenta> listaCuentas = new List<Cuenta>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Cuenta cuentaTemp = new Cuenta();
                        cuentaTemp.cuentaID = reader.GetInt32(0);
                        cuentaTemp.tipo = reader.GetString(1);
                        cuentaTemp.fechaCreacion = reader.GetDateTime(2);
                        cuentaTemp.FK_clienteID = reader.GetInt32(3);
                        cuentaTemp.FK_nombreUsuario = reader.GetString(4);
                        listaCuentas.Add(cuentaTemp);
                    }
                }
                else
                {
                    return listaCuentas;
                }
                reader.Close();
                return listaCuentas;
            }
        }


        /// <summary>
        /// Método que devuelve una cuenta en específico mediante el identificador del Cliente que viene de la capa lógica de negocio.
        /// </summary>
        /// <param name="clienteID"></param>
        /// <returns>Retorna una cuenta con todos los datos respectivos.</returns>
        public Cuenta obtenerCuenta(int clienteID)
        {
            SqlConnection connection = new SqlConnection(@connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand(
                  "SELECT cuentaID, tipo, fechaCreacion, FK_clienteID, FK_nombreUsuario FROM Facturacion.Cuenta WHERE FK_clienteID = @clienteID;",
                  connection);

                command.Parameters.Add("@clienteID", SqlDbType.Int);
                command.Parameters["@clienteID"].Value = clienteID;

                Cuenta cuentaTemp = new Cuenta();
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        cuentaTemp.cuentaID = reader.GetInt32(0);
                        cuentaTemp.tipo = reader.GetString(1);
                        cuentaTemp.fechaCreacion = reader.GetDateTime(2);
                        cuentaTemp.FK_clienteID = reader.GetInt32(3);
                        cuentaTemp.FK_nombreUsuario = reader.GetString(4);
                    }
                }
                else
                {
                    return null;
                }
                reader.Close();
                return cuentaTemp;
            }
        }

        /// <summary>
        /// Método encargado de modificar el saldo de la cuenta en la base de datos, con el nuevo saldo que viene de 
        /// la capa lógica de negocio y que tenga el identificador de la factura
        /// </summary>
        /// <param name="facturaID"></param>
        /// <param name="nuevoSaldo"></param>
        /// <returns>Retorna un valor booleano true si se realizó exitosamente el cambio, de lo contrario se retórna un false.</returns>
        public bool abonarCuenta(int facturaID, decimal nuevoSaldo)
        {
            try
            {
                SqlConnection connection = new SqlConnection(@connectionString);
                SqlCommand command = new SqlCommand(
                      "UPDATE Facturacion.Factura SET saldo = @nuevoSaldo WHERE facturaID = @facturaID;",
                      connection);
                command.Parameters.Add("@nuevoSaldo", SqlDbType.Money);
                command.Parameters["@nuevoSaldo"].Value = nuevoSaldo;
                command.Parameters.Add("@facturaID", SqlDbType.Int);
                command.Parameters["@facturaID"].Value = facturaID;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }



        //APARTADO DE FACTURA

        /// <summary>
        /// Método encargado de agregar una factura a la base de datos con todos los datos que 
        /// vienen de la capa de negocios.
        /// </summary>
        /// <param name="descuento"></param>
        /// <param name="total"></param>
        /// <param name="saldo"></param>
        /// <param name="FKcuentaID"></param>
        /// <returns>Retorna el índice de la factura agregada.</returns>
        public int crearFactura(int descuento, decimal total, decimal saldo, int FKcuentaID)
        {
            try
            {
                int idFactura = 0;
                SqlConnection connection = new SqlConnection(@connectionString);

                string sqlInsert = "INSERT INTO Facturacion.Factura(fecha, descuento, total, saldo, FK_cuentaID, activoSN) VALUES (@fecha, @descuento, @total, @saldo, @fkCuentaID, @activoSN)";
                string sqlReader = "SELECT MAX(facturaID) FROM Facturacion.Factura";

                SqlCommand insertCommand = new SqlCommand(sqlInsert, connection);
                SqlCommand readerCommand = new SqlCommand(sqlReader, connection);

                insertCommand.Parameters.Add("@fecha", SqlDbType.DateTime);
                insertCommand.Parameters["@fecha"].Value = DateTime.Now;

                insertCommand.Parameters.Add("@descuento", SqlDbType.Int);
                insertCommand.Parameters["@descuento"].Value = descuento;

                insertCommand.Parameters.Add("@total", SqlDbType.Decimal);
                insertCommand.Parameters["@total"].Value = total;

                insertCommand.Parameters.Add("@saldo", SqlDbType.Decimal);
                insertCommand.Parameters["@saldo"].Value = saldo;

                insertCommand.Parameters.Add("@fkCuentaID", SqlDbType.Int);
                insertCommand.Parameters["@fkCuentaID"].Value = FKcuentaID;

                insertCommand.Parameters.Add("@activoSN", SqlDbType.Bit);
                insertCommand.Parameters["@activoSN"].Value = true;

                connection.Open();
                insertCommand.ExecuteNonQuery();
                SqlDataReader reader = readerCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        idFactura = reader.GetInt32(0);
                    }

                }
                connection.Close();
                reader.Close();
                return idFactura;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// Método encargado de leer todas las facturas de la base de datos, además de enlistarlas para poder retornarlas.
        /// </summary>
        /// <returns>Retorna una lista del tipo Factura.</returns>
        public List<Factura> obtenerFacturas()
        {
            SqlConnection connection = new SqlConnection(@connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand(
                  "SELECT facturaID, fecha, descuento, total, saldo, FK_cuentaID, FK_InformeID, activoSN FROM Facturacion.Factura;",
                  connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                List<Factura> listaFacturas = new List<Factura>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Factura facturaTemp = new Factura();
                        facturaTemp.facturaID = reader.GetInt32(0);
                        facturaTemp.fecha = reader.GetDateTime(1);
                        facturaTemp.descuento = reader.GetByte(2);
                        facturaTemp.total = reader.GetDecimal(3);
                        facturaTemp.saldo = reader.GetDecimal(4);
                        facturaTemp.FK_cuentaID = reader.GetInt32(5);
                        facturaTemp.activoSN = reader.GetBoolean(7);
                        try
                        {
                            facturaTemp.FK_informeID = reader.GetInt32(6);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            facturaTemp.FK_informeID = 0;
                        }
                        listaFacturas.Add(facturaTemp);
                    }
                }
                else
                {
                    return listaFacturas;
                }
                reader.Close();
                return listaFacturas;
            }
        }

        /// <summary>
        /// Método encargado de agregar una linea de pedido con los datos que se enviaron desde la capa de negocios, ademas
        /// de que trae el identificador de la factura para poder relacionarlo.
        /// </summary>
        /// <param name="subtotal"></param>
        /// <param name="cantidad"></param>
        /// <param name="descuento"></param>
        /// <param name="fkFacturaID"></param>
        /// <param name="fkProductoID"></param>
        /// <param name="fkBodegaID"></param>
        /// <returns>Retorna un valor booleano true si se realizó con éxito, de lo contario retorna un false.</returns>
        public bool crearLineaPedido(decimal subtotal, int cantidad, int descuento, int fkFacturaID, int fkProductoID, int fkBodegaID)
        {
            try
            {
                int nuevaCantidad = obtenerCantidadProductoEsp(fkBodegaID, fkProductoID);
                nuevaCantidad = nuevaCantidad - cantidad;
                SqlConnection connection = new SqlConnection(@connectionString);

                string sqlInsert = "INSERT INTO Facturacion.LineaPedido(subtotal, cantidad, descuento, FK_facturaID, FK_productoID) VALUES (@subtotal, @cantidad, @descuento, @fkFacturaID, @fkProductoID)";
                SqlCommand commandUpdate = new SqlCommand(
                     "UPDATE Inventario.BodegaProducto SET cantidad = @cantidad WHERE FK_productoID = @id AND FK_bodegaID = @fkBodega;", connection);
                SqlCommand insertCommand = new SqlCommand(sqlInsert, connection);

                insertCommand.Parameters.Add("@subtotal", SqlDbType.Decimal);
                insertCommand.Parameters["@subtotal"].Value = subtotal;

                insertCommand.Parameters.Add("@cantidad", SqlDbType.Int);
                insertCommand.Parameters["@cantidad"].Value = cantidad;

                insertCommand.Parameters.Add("@descuento", SqlDbType.Int);
                insertCommand.Parameters["@descuento"].Value = descuento;

                insertCommand.Parameters.Add("@fkFacturaID", SqlDbType.Int);
                insertCommand.Parameters["@fkFacturaID"].Value = fkFacturaID;

                insertCommand.Parameters.Add("@fkProductoID", SqlDbType.Int);
                insertCommand.Parameters["@fkProductoID"].Value = fkProductoID;

                commandUpdate.Parameters.Add("@cantidad", SqlDbType.Int);
                commandUpdate.Parameters["@cantidad"].Value = nuevaCantidad;

                commandUpdate.Parameters.Add("@fkBodega", SqlDbType.Int);
                commandUpdate.Parameters["@fkBodega"].Value = fkBodegaID;

                commandUpdate.Parameters.Add("@id", SqlDbType.Int);
                commandUpdate.Parameters["@id"].Value = fkProductoID;

                connection.Open();
                insertCommand.ExecuteNonQuery();
                commandUpdate.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Método que lee desde la base de datos todas las lineas de pedido guardadas en esta, además las guarda en una lista del
        /// tipo LineaPedido.
        /// </summary>
        /// <returns>Retorna la lista con todas las lineas de pedido.</returns>
        public List<LineaPedido> obtenerLineasPedidos()
        {
            SqlConnection connection = new SqlConnection(@connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand(
                  "SELECT lineaPedidoID, subtotal, cantidad, descuento, FK_facturaID, FK_productoID FROM Facturacion.LineaPedido;",
                  connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                List<LineaPedido> listaLineaPedidos = new List<LineaPedido>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        LineaPedido lineaTemp = new LineaPedido();
                        lineaTemp.lineaPedidoID = reader.GetInt32(0);
                        lineaTemp.subtotal = reader.GetDecimal(1);
                        lineaTemp.cantidad = reader.GetInt32(2);
                        lineaTemp.descuento = reader.GetInt32(3);
                        lineaTemp.FK_facturaID = reader.GetInt32(4);
                        lineaTemp.FK_productoID = reader.GetInt32(5);

                        listaLineaPedidos.Add(lineaTemp);
                    }
                }
                else
                {
                    return listaLineaPedidos;
                }
                reader.Close();
                return listaLineaPedidos;
            }
        }

        /// <summary>
        /// Método encargado de realizar un borrado lógico de una factura mediante el identificador de la factura que 
        /// se ingresó en la capa de negocios.
        /// </summary>
        /// <param name="facturaID"></param>
        /// <returns>Retorna un valor booleano true si se realizó el borrado lógico con éxito, de lo contrario retorna un false.</returns>
        public bool eliminarFactura(int facturaID)
        {
            try
            {
                SqlConnection connection = new SqlConnection(@connectionString);
                SqlCommand command = new SqlCommand(
                      "UPDATE Facturacion.Factura SET activoSN = @activoSN WHERE facturaID = @facturaID",
                      connection);


                command.Parameters.Add("@facturaID", SqlDbType.Int);
                command.Parameters["@facturaID"].Value = facturaID;

                command.Parameters.Add("@activoSN", SqlDbType.Bit);
                command.Parameters["@activoSN"].Value = false;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Método encargado de agregar un informe a la base de datos con todos los datos que vienen desde la capa
        /// de negocio, además con el respectivo identificador del usuario que lo realizó para poder relacionarlo.
        /// </summary>
        /// <param name="saldo"></param>
        /// <param name="total"></param>
        /// <param name="fkUsuario"></param>
        /// <returns></returns>
        public int agregarInforme(decimal saldo, decimal total, string fkUsuario)
        {
            int idInforme = -1;
            SqlConnection connection = new SqlConnection(@connectionString);

            string sqlInsert = "INSERT INTO Facturacion.Informe(fecha, saldo, total, FK_nombreUsuario) VALUES (GETDATE(), @saldo, @total, @fkUsuario)";
            string sqlReader = "SELECT MAX(informeID) FROM Facturacion.Informe";

            SqlCommand insertCommand = new SqlCommand(sqlInsert, connection);
            SqlCommand readerCommand = new SqlCommand(sqlReader, connection);

            insertCommand.Parameters.Add("@saldo", SqlDbType.Decimal);
            insertCommand.Parameters["@saldo"].Value = saldo;

            insertCommand.Parameters.Add("@total", SqlDbType.Decimal);
            insertCommand.Parameters["@total"].Value = total;

            insertCommand.Parameters.Add("@fkUsuario", SqlDbType.NVarChar, 50);
            insertCommand.Parameters["@fkUsuario"].Value = fkUsuario;

            connection.Open();
            insertCommand.ExecuteNonQuery();
            SqlDataReader reader = readerCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    idInforme = reader.GetInt32(0);
                }

            }
            connection.Close();
            reader.Close();
            return idInforme;
        }

        /// <summary>
        /// Método encargado de agregarle a la tabla factura el identificador del informe al que está relacionada.
        /// </summary>
        /// <param name="facturaID"></param>
        /// <param name="fkInforme"></param>
        public void asignarFkinforme(int facturaID, int fkInforme)
        {
            SqlConnection connection = new SqlConnection(@connectionString);

            string sqlUpdate = "UPDATE Facturacion.Factura SET FK_informeID = @fkInforme WHERE facturaID = @id";

            SqlCommand updateCommand = new SqlCommand(sqlUpdate, connection);

            updateCommand.Parameters.Add("@fkInforme", SqlDbType.Int);
            updateCommand.Parameters["@fkInforme"].Value = fkInforme;

            updateCommand.Parameters.Add("@id", SqlDbType.Int);
            updateCommand.Parameters["@id"].Value = facturaID;

            connection.Open();
            updateCommand.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Método encargado de buscar en la base de datos el informe realizado por un agente de ventas, mediante 
        /// el nombre de usuario que viene desde la capa de negocio.
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <returns>Retorna el informe que se encontró en la base de datos.</returns>
        public Informe obtenerInformeXAgente(string nombreUsuario)
        {
            Informe nuevoInforme = new Informe();
            SqlConnection connection = new SqlConnection(@connectionString);

            string sqlSelect = "SELECT TOP(1)*  FROM Facturacion.Informe WHERE FK_nombreUsuario = @nombre ORDER BY Facturacion.Informe.fecha DESC";

            SqlCommand updateCommand = new SqlCommand(sqlSelect, connection);

            updateCommand.Parameters.Add("@nombre", SqlDbType.NVarChar, 50);
            updateCommand.Parameters["@nombre"].Value = nombreUsuario;

            connection.Open();
            SqlDataReader reader = updateCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    nuevoInforme.informeID = reader.GetInt32(0);
                    nuevoInforme.fecha = reader.GetDateTime(1);
                    nuevoInforme.saldo = reader.GetDecimal(2);
                    nuevoInforme.total = reader.GetDecimal(3);
                    nuevoInforme.FK_nombreUsuario = reader.GetString(4);
                }
            }
            reader.Close();
            return nuevoInforme;
        }
    }
}