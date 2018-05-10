function main() {
    let button = new Button();
    button.Padding = new Thickness(1.5);
    button.Content = "Hello, JavaScript " + (0.1 + 0.2);
    button.Click.connect((o, e) => { MessageBox.Show("Hey there!"); });
    content.FindName("grid").Children.Add(button);
} 

main();