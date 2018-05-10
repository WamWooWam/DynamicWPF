function main() {
    let document = content.FindName("document");
    let paragraph = new Paragraph();
    paragraph.Inlines.Add(new Run("DynamicWPF also has JavaScript-based scripting, exposing almost the entire WPF API to scripts. In fact, the paragraph you're reading right now was added with JS!"));
    document.Blocks.InsertBefore(document.Blocks.LastBlock.PreviousBlock, paragraph);
}

main();