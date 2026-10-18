using PITSysCodeGenerator.Models; using System.Collections.ObjectModel;
namespace PITSysCodeGenerator.Dialogs {
public class GeneratorViewModel : System.ComponentModel.INotifyPropertyChanged {
public ObservableCollection<ContractField> Fields { get; set; } = new ObservableCollection<ContractField>();
public System.EventHandler<System.ComponentModel.PropertyChangedEventArgs> PropertyChanged;
public GeneratorOptions ToGeneratorOptions() => new GeneratorOptions { BaseClassName = "Test" };
}}
