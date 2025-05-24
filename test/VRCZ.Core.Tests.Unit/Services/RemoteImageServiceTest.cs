using Microsoft.Extensions.Caching.Memory;
using VRCZ.Core.Services;

namespace VRCZ.Core.Tests.Unit.Services;

public class RemoteImageServiceTest
{
    private readonly RemoteImageService _remoteImageService;
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;

    public RemoteImageServiceTest()
    {
        _httpClient = new HttpClient();
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
        _remoteImageService = new RemoteImageService(_httpClient, _memoryCache);
    }

    private const string TestVRChatImagePath =
        "/api/1/image/file_0c853d9f-fb1d-4e38-8d35-57fe94949269/1/256";

    private const string TestVRChatFilePath =
        "/api/1/file/file_0c853d9f-fb1d-4e38-8d35-57fe94949269/1/file/";

    private const string TestVRChatImageUrl =
        "https://vrchat.com/api/1/image/file_0c853d9f-fb1d-4e38-8d35-57fe94949269/1/256";
    private const string TestVRChatFileUrl =
        "https://vrchat.com/api/1/file/file_0c853d9f-fb1d-4e38-8d35-57fe94949269/1/file/";
    private const string TestVRChatFileUrlCloud =
        "https://api.vrchat.cloud/api/1/file/file_0c853d9f-fb1d-4e38-8d35-57fe94949269/1/file/";
    private const string TestVRChatImageUrlCloud =
        "https://api.vrchat.cloud/api/1/image/file_0c853d9f-fb1d-4e38-8d35-57fe94949269/1/256";

    private const string NonVRChatImageUrl = "https://picsum.photos/200";

    private const string RandomVRChatHostUrl = "https://vrchat.com/random-path";
    private const string RandomVRChatCloudHostUrl = "https://api.vrchat.cloud/random-path";

    [Fact]
    public void GetMemoryCacheKey_ShouldReturnCorrectKey()
    {
        // Arrange
        var url = "https://example.com/image.png";
        var expectedKey = RemoteImageService.MemoryCacheKeyPrefix + url;

        // Act
        var key = _remoteImageService.GetMemoryCacheKey(url);

        // Assert
        Assert.Equal(expectedKey, key);
    }

    #region GetVRChatFileCacheKey

    private static readonly string[] ExpectedVRChatImageFileCacheKeyParts =
        ["vrchat-files", "file_0c853d9f-fb1d-4e38-8d35-57fe94949269_f8c50", "1_c4ca4", "256_f7184"];

    private static readonly string ExpectedVRChatImageFileCacheKey = Path.Join(ExpectedVRChatImageFileCacheKeyParts);

    #region File Path

    private static readonly string[] ExpectedVRChatFileCacheKeyParts =
        ["vrchat-files", "file_0c853d9f-fb1d-4e38-8d35-57fe94949269_f8c50", "1_c4ca4", "file_8c7dd"];

    private static readonly string ExpectedVRChatFileCacheKey = Path.Join(ExpectedVRChatFileCacheKeyParts);

    #region TestVRChatFilePath Variants

    private const string TestVRChatFilePathWithoutLastSlash =
        "/api/1/file/file_0c853d9f-fb1d-4e38-8d35-57fe94949269/1/file";

    private const string TestVRChatFilePathWithoutFileEnding =
        "/api/1/file/file_0c853d9f-fb1d-4e38-8d35-57fe94949269/1/";

    private const string TestVRChatFilePathWithoutFileEndingAndLastSlash =
        "/api/1/file/file_0c853d9f-fb1d-4e38-8d35-57fe94949269/1";

    private const string TestVRChatFilePathWithDoubleSlashAndFileEnding =
        "/api/1/file/file_0c853d9f-fb1d-4e38-8d35-57fe94949269//1/file";

    private const string TestVRChatFilePathWithDoubleSlash =
        "/api/1/file/file_0c853d9f-fb1d-4e38-8d35-57fe94949269//1/";

    #endregion

    [Fact]
    public void GetVRChatFileCacheKey_FilePath_ShouldReturnCorrectKey()
    {
        // Arrange
        var expectedKey = ExpectedVRChatFileCacheKey;

        // Act
        var key = _remoteImageService.GetVRChatFileCacheKey(TestVRChatFilePath);

        // Assert
        Assert.Equal(expectedKey, key);
    }

    [Fact]
    public void GetVRChatFileCacheKey_FilePathWithoutLastSlash_ShouldReturnCorrectKey()
    {
        // Arrange
        var expectedKey = ExpectedVRChatFileCacheKey;

        // Act
        var key = _remoteImageService.GetVRChatFileCacheKey(TestVRChatFilePathWithoutLastSlash);

        // Assert
        Assert.Equal(expectedKey, key);
    }

    [Fact]
    public void GetVRChatFileCacheKey_FilePathWithoutFileEnding_ShouldReturnCorrectKey()
    {
        // Arrange
        var expectedKey = ExpectedVRChatFileCacheKey;

        // Act
        var key = _remoteImageService.GetVRChatFileCacheKey(TestVRChatFilePathWithoutFileEnding);

        // Assert
        Assert.Equal(expectedKey, key);
    }

    [Fact]
    public void GetVRChatFileCacheKey_FilePathWithoutFileEndingAndLastSlash_ShouldReturnCorrectKey()
    {
        // Arrange
        var expectedKey = ExpectedVRChatFileCacheKey;

        // Act
        var key = _remoteImageService.GetVRChatFileCacheKey(TestVRChatFilePathWithoutFileEndingAndLastSlash);

        // Assert
        Assert.Equal(expectedKey, key);
    }

    [Fact]
    public void GetVRChatFileCacheKey_FilePathWithDoubleSlash_ShouldReturnCorrectKey()
    {
        // Arrange
        var expectedKey = ExpectedVRChatFileCacheKey;

        // Act
        var key = _remoteImageService.GetVRChatFileCacheKey(TestVRChatFilePathWithDoubleSlash);

        // Assert
        Assert.Equal(expectedKey, key);
    }

    [Fact]
    public void GetVRChatFileCacheKey_FilePathWithDoubleSlashAndFileEnding_ShouldReturnCorrectKey()
    {
        // Arrange
        var expectedKey = ExpectedVRChatFileCacheKey;

        // Act
        var key = _remoteImageService.GetVRChatFileCacheKey(TestVRChatFilePathWithDoubleSlashAndFileEnding);

        // Assert
        Assert.Equal(expectedKey, key);
    }

    #endregion

    #region Image Path

    private const string TestVRChatImagePathWithoutLastSlash =
        "/api/1/image/file_0c853d9f-fb1d-4e38-8d35-57fe94949269/1/256";

    private const string TestVRChatImagePathWithDoubleSlash =
        "/api/1/image/file_0c853d9f-fb1d-4e38-8d35-57fe94949269//1/256";

    [Fact]
    public void GetVRChatFileCacheKey_ImagePath_ShouldReturnCorrectKey()
    {
        // Arrange
        var expectedKey = ExpectedVRChatImageFileCacheKey;

        // Act
        var key = _remoteImageService.GetVRChatFileCacheKey(TestVRChatImagePath);

        // Assert
        Assert.Equal(expectedKey, key);
    }

    [Fact]
    public void GetVRChatFileCacheKey_ImagePathWithoutLastSlash_ShouldReturnCorrectKey()
    {
        // Arrange
        var expectedKey = ExpectedVRChatImageFileCacheKey;

        // Act
        var key = _remoteImageService.GetVRChatFileCacheKey(TestVRChatImagePathWithoutLastSlash);

        // Assert
        Assert.Equal(expectedKey, key);
    }

    [Fact]
    public void GetVRChatFileCacheKey_ImagePathWithDoubleSlash_ShouldReturnCorrectKey()
    {
        // Arrange
        var expectedKey = ExpectedVRChatImageFileCacheKey;

        // Act
        var key = _remoteImageService.GetVRChatFileCacheKey(TestVRChatImagePathWithDoubleSlash);

        // Assert
        Assert.Equal(expectedKey, key);
    }

    #endregion

    private static readonly string[] ExpectedVRChatFileCacheKeyWithInvalidPathCharactersParts =
        ["vrchat-files", "%3c%3e%22%7c%5c_b7bef", "1_c4ca4", "file_8c7dd"];

    private static readonly string ExpectedVRChatFileCacheKeyWithInvalidPathCharacters =
        Path.Join(ExpectedVRChatFileCacheKeyWithInvalidPathCharactersParts);

    private const string TestVRChatFilePathWithInvalidPathCharacters =
        "/api/1/file/<>\"|\\/1/file/";

    [Fact]
    public void GetVRChatFileCacheKey_WithInvalidPathCharacters_ShouldReturnCorrectKey()
    {
        // Arrange
        var expectedKey = ExpectedVRChatFileCacheKeyWithInvalidPathCharacters;

        // Act
        var key = _remoteImageService.GetVRChatFileCacheKey(TestVRChatFilePathWithInvalidPathCharacters);

        // Assert
        Assert.Equal(expectedKey, key);
    }

    #endregion

    #region MD5

    [Fact]
    public void GetShortMD5_ShouldReturnCorrectShortMD5()
    {
        // Arrange
        const string source = "test";
        const string expectedShortMD5 = "098f6";

        // Act
        var shortMD5 = RemoteImageService.GetShortMD5(source);

        // Assert
        Assert.Equal(expectedShortMD5, shortMD5);
    }

    [Fact]
    public void GetMD5_ShouldReturnCorrectMD5()
    {
        // Arrange
        const string source = "test";
        const string expectedMD5 = "098f6bcd4621d373cade4e832627b4f6";

        // Act
        var md5 = RemoteImageService.GetMD5(source);

        // Assert
        Assert.Equal(expectedMD5, md5);
    }

    #endregion

    #region IsVRChatFiles

    [Fact]
    public void IsVRChatFiles_ShouldReturnTrue_ForValidVRChatImageUrl()
    {
        // Arrange
        var uri = new Uri(TestVRChatImageUrl);

        // Act
        var result = _remoteImageService.IsVRChatFiles(uri);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsVRChatFiles_ShouldReturnTrue_ForValidVRChatImageUrlCloud()
    {
        // Arrange
        var uri = new Uri(TestVRChatImageUrlCloud);

        // Act
        var result = _remoteImageService.IsVRChatFiles(uri);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsVRChatFiles_ShouldReturnTrue_ForValidVRChatFileUrl()
    {
        // Arrange
        var uri = new Uri(TestVRChatFileUrl);

        // Act
        var result = _remoteImageService.IsVRChatFiles(uri);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsVRChatFiles_ShouldReturnTrue_ForValidVRChatFileUrlCloud()
    {
        // Arrange
        var uri = new Uri(TestVRChatFileUrlCloud);

        // Act
        var result = _remoteImageService.IsVRChatFiles(uri);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsVRChatFiles_ShouldReturnFalse_ForNonVRChatImageUrl()
    {
        // Arrange
        var uri = new Uri(NonVRChatImageUrl);

        // Act
        var result = _remoteImageService.IsVRChatFiles(uri);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsVRChatFiles_ShouldReturnFalse_ForRandomVRChatHostUrl()
    {
        // Arrange
        var uri = new Uri(RandomVRChatHostUrl);

        // Act
        var result = _remoteImageService.IsVRChatFiles(uri);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsVRChatFiles_ShouldReturnFalse_ForRandomVRChatCloudHostUrl()
    {
        // Arrange
        var uri = new Uri(RandomVRChatCloudHostUrl);

        // Act
        var result = _remoteImageService.IsVRChatFiles(uri);

        // Assert
        Assert.False(result);
    }

    #endregion
}
