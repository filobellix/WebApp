using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApp.Pages
{
	public class testModel : PageModel
	{
		public string errorMessage = "";

		public class anagrafica
		{
			public int id;
			public string nome;
			public string cognome;
			public string dataNascita;
			public string telefono;
		}

		public List<anagrafica> lista = new List<anagrafica>();
		public void OnGet()
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                                      "AttachDbFileName=C:\\Users\\bellesef\\Downloads\\WebApp\\WebApp\\WebApp\\db.mdf;" +
									  "Integrated Security=True;" +
									  "Connect Timeout=30";

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					String sql = "SELECT * FROM Anagrafica";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								anagrafica dato = new anagrafica();

								dato.id = reader.GetInt32(0);
								dato.nome = reader.GetString(1);
								dato.cognome = reader.GetString(2);
								dato.dataNascita = reader.GetDateTime(3).ToString("MM/dd/yyyy");
								dato.telefono = reader.GetString(4);

								lista.Add(dato);
							}
						}
					}

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
