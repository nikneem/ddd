using HexMaster.DomainDrivenDesign.ValueObjects;
using Xunit;

namespace HexMaster.DomainDrivenDesign.UnitTests.ValueObjects;

public class PasswordTests
{
    [Fact]
    public void WhenPasswordIsGenerated_ItVerifiesSuccesfully()
    {
        var plainTextPassword = "PlainTextPassword";
        var passwordObject = new Password(plainTextPassword);
        Assert.True(passwordObject.Verify(plainTextPassword));
    }

    [Fact]
    public void WhenPasswordReCreated_ItStillVerifies()
    {
        var plainTextPassword = "PlainTextPassword";
        var passwordObject = new Password(plainTextPassword);
        var copiedObject = new Password(passwordObject.EncryptedPassword, passwordObject.Secret);
        Assert.True(copiedObject.Verify(plainTextPassword));
    }
}