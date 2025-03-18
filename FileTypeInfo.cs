namespace FileHeaderCheck;

/// <summary>
/// 文件类型信息
/// </summary>
public class FileTypeInfo
{
    /// <summary>
    /// 魔数
    /// </summary>
    public byte[]? MagicBytes { get; init; }
    /// <summary>
    /// 第二段魔数
    /// </summary>
    public byte[]? MagicBytes2 { get; init; }
    /// <summary>
    /// 第二段魔数偏移量    
    /// <summary>
    public int OffSet2 { get; init; }
    /// 文件类型名称
    /// </summary>
    public string Name { get; init; }
    /// <summary>
    /// 文件扩展名
    /// </summary>
    public string Extension { get; init; }
    /// <summary>
    /// MIME类型
    /// </summary>
    public string? Mime { get; init; }
    /// <summary>
    /// 偏移量
    /// </summary>
    public int Offset { get; init; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="magicBytes"></param>
    /// <param name="magicBytes2"></param>
    /// <param name="offSet2"></param>
    /// <param name="name"></param>
    /// <param name="extension"></param>
    /// <param name="mime"></param>
    /// <param name="offSet"></param>
    public FileTypeInfo(string name, string extension, byte[]? magicBytes, string? mime = null, int offSet = 0, byte[]? magicBytes2 = null, int offSet2 = 0)
    {
        MagicBytes = magicBytes;
        MagicBytes2 = magicBytes2;
        OffSet2 = offSet2;
        Name = name;
        Extension = extension;
        Mime = mime;
        Offset = offSet;
    }
}
