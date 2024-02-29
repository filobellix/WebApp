using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static WebApp.Pages.testModel;
using System.Data.SqlClient;

namespace WebApp.Pages
{
	public class modificaModel : PageModel
    {
		public string errorMessage = "";

		String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                                      "AttachDbFileName=C:\\Users\\bellesef\\Downloads\\WebApp\\WebApp\\WebApp\\db.mdf;" +
									  "Integrated Security=True;" +
									  "Connect Timeout=30";

		public anagrafica dato = new anagrafica();

		public void OnGet() //questa viene eseguita al caricamento della pagina
        {
            String id = "" + Request.Query["id"];

            try
            {
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					String sql = $"SELECT * FROM Anagrafica WHERE id like '{id}'";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								dato.id = reader.GetInt32(0);
								dato.nome = reader.GetString(1);
								dato.cognome = reader.GetString(2);
								dato.dataNascita = reader.GetDateTime(3).ToString("MM/dd/yyyy");
								dato.telefono = reader.GetString(4);

							}
						}
					}

					connection.Close();
				}
			}
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost() //questa viene eseguita al salvare della pagina
        {
            dato.nome = Request.Query["nome"];
            dato.cognome = Request.Query["cognome"];
            dato.dataNascita = Request.Query["dataNascita"];
            dato.telefono = Request.Query["telefono"];

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = $"UPDATE Anagrafica SET nome = {dato.nome}, cognome = {dato.cognome}, dataNascita = {dato.dataNascita}, telefono = '{dato.telefono}';";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                      
                    }

                    Response.Redirect("test");

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
