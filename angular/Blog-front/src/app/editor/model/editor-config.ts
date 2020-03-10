export class EditorConfig {
    public width = '100%';
    public height = '400';
    public path = 'assets/editor.md/lib/';
    public codeFold: true;
    public searchReplace = true;
    public toolbar = true;
    public emoji = true;
    public taskList = true;
    public tex = true;
    public readOnly = false;
    public tocm = true;
    public watch = true;
    public previewCodeHighlight = true;
    public saveHTMLToTextarea = true;
    public markdown = '';
    public flowChart = true;
    public syncScrolling = true;
    public sequenceDiagram = true;
    public imageUpload = true;
    public imageFormats = ['jpg', 'jpeg', 'gif', 'png', 'bmp', 'webp'];
    public imageUploadURL = '';
    public htmlDecode = 'style,script,iframe';  // you can filter tags decode
    public editorFunction = '';//定义调用的方法
    constructor() {
    }
}