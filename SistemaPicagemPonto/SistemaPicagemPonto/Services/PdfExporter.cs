using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Fonts;
using SistemaPicagemPonto.Interfaces;

namespace SistemaPicagemPonto.Services;

public static class PdfExporter
{
    public static void Exportar(List<IRegistoPonto> historico, int colaboradorId)
    {
        try
        {
            string nomeFicheiro =
                $"registos_colaborador_{colaboradorId}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

            PdfDocument documento = new();
            documento.Info.Title = "Relatório de Picagens";

            PdfPage pagina = documento.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(pagina);

            if (GlobalFontSettings.FontResolver == null)
            {
                GlobalFontSettings.FontResolver = new CustomFontResolver();
            }

            XFont titulo = new("Arial", 16);
            XFont normal = new("Arial", 10);

            double y = 40;

            gfx.DrawString(
                $"Relatório de Picagens - Colaborador {colaboradorId}",
                titulo,
                XBrushes.Black,
                new XPoint(40, y));

            y += 40;

            foreach (IRegistoPonto registo in historico)
            {
                string entrada = registo.HoraEntrada?.ToString("HH:mm:ss") ?? "-";
                string saida = registo.HoraSaida?.ToString("HH:mm:ss") ?? "-";

                gfx.DrawString(
                    $"{registo.Data:dd/MM/yyyy} | Entrada: {entrada} | Saída: {saida}",
                    normal,
                    XBrushes.Black,
                    new XPoint(40, y));

                y += 20;
            }

            documento.Save(nomeFicheiro);
            documento.Close();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Falha na geração do ficheiro PDF.", ex);
        }
    }
}