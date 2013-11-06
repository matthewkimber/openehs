using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace OpenEhs.Data
{
    public static class MySqlUtilities
    {
        /// <summary>
        /// Backup a schema to a specific directory
        /// </summary>
        /// <param name="schemaName">schema to backup</param>
        /// <param name="rootPassword">root password of the database</param>
        /// <param name="destinationDirectory">directory that you want to copy the backup to</param>
        public static void Backup(string schemaName, string rootPassword, string destinationDirectory)
        {
            var mySqlCommandLineTool = ConfigurationManager.AppSettings["MySql.BinDirectory"];

            var process = new Process
                              {
                                  StartInfo =
                                      {
                                          FileName = Path.Combine(mySqlCommandLineTool, "mysqldump.exe"),
                                          Arguments =
                                              String.Format(
                                                  "--user=root --password={0} --add-drop-database --add-drop-table {1}",
                                                  rootPassword, schemaName),
                                          UseShellExecute = false,
                                          RedirectStandardOutput = true,
                                          RedirectStandardInput = true,
                                          RedirectStandardError = true,
                                          CreateNoWindow = true
                                      }
                              };

            process.ErrorDataReceived += process_ErrorDataReceived;
            process.Start();

            var backupData = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            string filename = Path.Combine(destinationDirectory, String.Format("OpenEHS-Backup({0}).bak", DateTime.Now.ToString("yyyyMMdd")));

            using (var fileOut = File.CreateText(filename))
            {
                fileOut.Write(backupData);
            }
        }

        /// <summary>
        /// Restore a schema from a specific backup file
        /// </summary>
        /// <param name="schemaName">schema to backup</param>
        /// <param name="rootPassword">root password of the database</param>
        /// <param name="backupFileName">directory that you want to copy the backup to</param>
        public static void Restore(string schemaName, string rootPassword, string backupFileName)
        {
            var mySqlCommandLineTool = ConfigurationManager.AppSettings["MySql.BinDirectory"];
            if (File.Exists(backupFileName))
            {
                string restoreData;

                using (var fileIn = File.OpenText(backupFileName))
                {
                    restoreData = fileIn.ReadToEnd();
                }

                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = Path.Combine(mySqlCommandLineTool, "mysql.exe"),
                        Arguments = String.Format("--user=root --password={0}", rootPassword),
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardInput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    }
                };

                process.Start();

                using (var stdin = process.StandardInput)
                {
                    stdin.Write(restoreData);
                    stdin.Close();
                }

                process.WaitForExit();
            }
        }

        /// <summary>
        /// Event that is called if an error occurs in the process of backing up or restoring a database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            // TODO: Create a custom exception for this scenario.
            throw new Exception("OUCH! Error dumping the database!");
        }
    }
}