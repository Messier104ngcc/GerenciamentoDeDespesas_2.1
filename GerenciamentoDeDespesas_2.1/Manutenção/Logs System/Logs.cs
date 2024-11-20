namespace GerenciamentoDeDespesas_2._1.Manutenção.Logs_System
{
    public class Logs : ILogs
    {
        void ILogs.RegistrarLogDeExcecao(Exception ex)
        {
            // Diretório onde o arquivo de log será salvo
            string caminhoDoLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            Directory.CreateDirectory(caminhoDoLog); // Garante que o diretório de log existe

            // Nome do arquivo com a data atual
            string nomeArquivo = Path.Combine(caminhoDoLog, $"log_{DateTime.Now:yyyyMMdd}.txt");

            // Texto do log com as informações da exceção
            string logTexto = $"Data: {DateTime.Now}\n" +
                              $"Mensagem: {ex.Message}\n" +
                              $"StackTrace: {ex.StackTrace}\n" +
                              $"Fonte: {ex.Source}\n" +
                              $"Método: {ex.TargetSite}\n" +
                              "----------------------------------------\n";

            // Salva o log em um arquivo de texto
            File.AppendAllText(nomeArquivo, logTexto);
        }
    }
}
