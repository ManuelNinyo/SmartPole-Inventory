using FluentAssertions;
using SmartPole.Inventory.App.ViewModels;
using Xunit;

namespace SmartPole.Inventory.UnitTests.ViewModels;

public class BaseViewModelTests {
  [Fact]
  public void IsBusy_ShouldNotifyIsNotBusy() {
    var vm = new BaseViewModel();
    vm.IsBusy = true;
    vm.IsNotBusy.Should().BeFalse();
    
    vm.IsBusy = false;
    vm.IsNotBusy.Should().BeTrue();
  }
}
