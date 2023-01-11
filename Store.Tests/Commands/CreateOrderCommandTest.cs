using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;

namespace Store.Tests.Commands;

[TestClass]
public class CreateOrderCommandTest
{
    [TestMethod]
    [TestCategory("Handlers")]
    public void DadoUmComandoInvalidoOPedidoNaoDeveSerGerado()
    {
        var command = new CreateOrderCommand();
        command.Customer = "";
        command.ZipCode = "12345678";
        command.PromoCode = "12345670";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Valideta();
        
        Assert.AreEqual(command.IsValid, true);
    }
}