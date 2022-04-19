using System;
using TextEditor.BL;
using TextEdtor;

namespace TextEditor
{
    public class MainPresenter
    {
        private readonly IMainForm _view;
        private readonly IFileManager _manager;
        private readonly IMessageService _messageService;

        private string _currentFilePath;

        public MainPresenter(IMainForm view, IFileManager manager, IMessageService service)
        {
            _view = view;
            _manager = manager;
            _messageService = service;

            _view.SetSymbolCount(0);

            _view.ContentChanged += new EventHandler(_view_ContentChanged);
            _view.FileOpenClick += new EventHandler(_view_FileOpenClick);
        }

        void _view_ContentChanged(object sender, EventArgs e)
        {
            string content = _view.Content;

            int count = _manager.GetSymbolCount(content);

            _view.SetSymbolCount(count);
        }

        void _view_FileOpenClick(object sender, EventArgs e)
        {
            try
            {
                string filePath = _view.FilePath;

                bool isExist = _manager.IsExist(filePath);

                if (!isExist)
                {
                    _messageService.ShwoExclamation("Selected file is not existed");
                    return;
                }

                _currentFilePath = filePath;

                string content = _manager.GetContent(filePath);
                int count = _manager.GetSymbolCount(content);

                _view.Content = content;
                _view.SetSymbolCount(count);
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }
    }
}
