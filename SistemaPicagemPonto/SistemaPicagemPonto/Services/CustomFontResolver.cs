using PdfSharp.Fonts;

namespace SistemaPicagemPonto.Services;

public class CustomFontResolver : IFontResolver
{
    public byte[] GetFont(string faceName)
    {
        string[] caminhosPossiveis =
        {
            "/usr/share/fonts/truetype/msttcorefonts/Arial.ttf",
            "/System/Library/Fonts/Supplemental/Arial.ttf"
        };

        string? caminhoFonte = caminhosPossiveis.FirstOrDefault(File.Exists);

        if (caminhoFonte == null)
        {
            throw new FileNotFoundException("Fonte necessária para gerar o PDF não encontrada.");
        }

        return File.ReadAllBytes(caminhoFonte);
    }
    
    public FontResolverInfo ResolveTypeface(
        string familyName,
        bool isBold,
        bool isItalic)
    {
        return new FontResolverInfo("Arial");
    }
}