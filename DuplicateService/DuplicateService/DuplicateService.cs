using MagazinAuto.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DuplicateService
{
    public class DuplicateService
    {
        private const string connectionString = "Server=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";
        public List<Car> GetCars()
        {
            var sql = "SELECT * FROM public.anunturi";
            var result = new List<Car>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Car
                            {
                                Id = (Guid)reader[0],
                                Caroserie = (Caroserie)decimal.ToInt32((decimal)reader[1]),
                                Cutie = (Cutie)decimal.ToInt32((decimal)reader[2]),
                                Transmisie = (Transmisie)decimal.ToInt32((decimal)reader[3]),
                                NormaPoluare = (NormaPoluare)decimal.ToInt32((decimal)reader[4]),
                                Combustibil = (Combustibil)decimal.ToInt32((decimal)reader[5]),
                                CP = decimal.ToInt32((decimal)reader[6]),
                                CapacitateCilindrica = decimal.ToInt32((decimal)reader[7]),
                                Km = decimal.ToInt32((decimal)reader[8]),
                                Pret = decimal.ToInt32((decimal)reader[9]),
                                AnFabricatie = decimal.ToInt32((decimal)reader[10]),
                                Marca = (string)reader[11],
                                Model = (string)reader[12],
                                Descriere = (string)reader[13]
                            });
                        }
                    }
                }
            }

            return result;
        }

        public List<Guid> GetDuplicates(List<Car> list)
        {
            var result = new List<Guid>();

            for(int i = 0; i < list.Count - 1; i++)
            {
                for(int j = i + 1; j < list.Count; j++)
                {
                    if(list[i].Caroserie == list[j].Caroserie &&
                        list[i].Cutie == list[j].Cutie &&
                        list[i].Transmisie == list[j].Transmisie &&
                        list[i].NormaPoluare == list[j].NormaPoluare &&
                        list[i].Combustibil == list[j].Combustibil &&
                        list[i].CP == list[j].CP &&
                        list[i].CapacitateCilindrica == list[j].CapacitateCilindrica &&
                        list[i].Km == list[j].Km &&
                        list[i].Pret == list[j].Pret &&
                        list[i].AnFabricatie == list[j].AnFabricatie &&
                        list[i].Marca == list[j].Marca &&
                        list[i].Model == list[j].Model &&
                        list[i].Descriere == list[j].Descriere)
                    {
                        result.Add(list[j].Id);
                    }
                }
            }

            return result.Distinct().ToList();
        }

        public void DeleteDuplicates(List<Guid> duplicates)
        {
            if(duplicates.Count == 0)
            {
                return;
            }

            var sql = "DELETE FROM public.anunturi WHERE id ";

            if(duplicates.Count == 1)
            {
                sql += $"= '{duplicates[0]}';";
            }
            else
            {
                sql += $"IN('{duplicates[0]}'";

                for(int i = 1; i < duplicates.Count; i++)
                {
                    sql += $", '{duplicates[i]}'";
                }

                sql += ");";
            }

            using (var connection = new NpgsqlConnection(connectionString))
            {
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
