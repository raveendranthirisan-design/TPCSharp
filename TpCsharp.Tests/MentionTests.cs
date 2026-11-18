namespace TpCsharp.Tests;

// Tests de la methode Mention
public class MentionTests
{
    // Theory = le meme test lance plusieurs fois, une fois par InlineData
    // Chaque InlineData est un cas : la moyenne puis la mention attendue
    [Theory]
    [InlineData(18, "Très bien")]
    [InlineData(15, "Bien")]
    [InlineData(13, "Assez bien")]
    [InlineData(11, "Passable")]
    [InlineData(5, "Ajourné")]
    public void TestMention(double moyenne, string attendu)
    {
        // on appelle la vraie methode Mention et on compare avec la mention attendue
        Assert.Equal(attendu, Program.Mention(moyenne));
    }
}
