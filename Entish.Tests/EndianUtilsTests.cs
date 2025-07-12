namespace Entish.Tests;

public class EndianUtilsTests
{
    [Fact]
    public void ShouldSwap_BigEndianOnLittleEndian_ShouldBeTrue()
    {
        EndianUtils.ShouldSwap(Endianness.Big).ShouldBe(true);
    }
    
    [Fact]
    public void ShouldSwap_LittleEndianOnLittleEndian_ShouldBeFalse()
    {
        EndianUtils.ShouldSwap(Endianness.Little).ShouldBe(false);
    }
}