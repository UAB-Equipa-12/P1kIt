using PdfSharp.Fonts;

namespace SistemaPicagemPonto.Services;

public class CustomFontResolver : IFontResolver
{
    public byte[] GetFont(string faceName)
    {
        string caminhoFonte = "/usr/share/fonts/truetype/msttcorefonts/Arial.ttf";
        
        if (!File.Exists(caminhoFonte))
        {
            throw new FileNotFoundException("Fonte necessária para gerar o PDF não encontrada.", caminhoFonte);
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