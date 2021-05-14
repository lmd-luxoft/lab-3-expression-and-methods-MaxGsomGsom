using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace XO
{
    [TestFixture]
    public class Tests
    {
        private void SetInput(params string[] input)
        {
            Console.SetIn(new StringReader(string.Join("\n", input)));
        }

        private string[] GetOutput(Action action)
        {
            var writer = new StringWriter();
            Console.SetOut(writer);
            action();
            return writer.ToString().Split(new []{ "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        [Test]
        public void Player1Wins()
        {
            SetInput(
                "Player1",
                "Player2",
                "1",
                "4",
                "2",
                "5",
                "3");
            
            var lines = GetOutput(() =>
            {
                Program.Main();
            });
            Assert.AreEqual("Player1 вы выиграли поздравляем Player2 а вы проиграли...", lines.Last());
        }

        [Test]
        public void Player2Wins()
        {
            SetInput(
                "Player1",
                "Player2",
                "1",
                "5",
                "9",
                "3",
                "4",
                "7");
            
            var lines = GetOutput(Program.Main);
            Assert.AreEqual("Player2 вы выиграли поздравляем Player1 а вы проиграли...", lines.Last());
        }
    }
}
