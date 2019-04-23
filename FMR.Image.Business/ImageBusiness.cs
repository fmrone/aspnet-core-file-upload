using FMR.Image.Business.Commands;
using FMR.Image.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMR.Image.Business
{
    public class ImageBusiness : IImageBusiness
    {
        private ImageDbContext _context;

        public ImageBusiness()
        {
        }

        public ImageBusiness(ImageDbContext context)
        {
            _context = context;
        }
        
        public CommandResult Create(Data.Entities.Image image)
        {
            var commandResult = ValidateFields(image);

            commandResult = ValidadeFileExtention(image);

            if (commandResult.Notifications.Count == 0)
            {
                commandResult = ValidadeQuantityOfImages(FindAll().ToList());

                if (commandResult.Notifications.Count == 0)
                {
                    _context.Images.Add(image);
                    _context.SaveChanges();
                }
            }

            return commandResult;
        }

        public CommandResult Delete(string id)
        {
            var commandResult = new CommandResult();

            var image = FindById(id);

            if (image == null)
            {
                commandResult.Notifications.Add("Imagem não encontrada");
            }
            else
            {
                _context.Images.Remove(image);
                _context.SaveChanges();
            }

            return commandResult;
        }

        public Data.Entities.Image FindById(string id)
        {
            return _context.Images.Where(q => q.Id == id).SingleOrDefault();
        }

        public IEnumerable<Data.Entities.Image> FindAll()
        {
            return _context.Images.ToList();
        }

        public CommandResult ValidateFields(Data.Entities.Image image)
        {
            var commandResult = new CommandResult();

            if (image == null)
            {
                commandResult.Notifications.Add("Preencha as informações da imagem");
            }
            else
            { 
                if (image.File == null)
                    commandResult.Notifications.Add("Nenhum arquivo foi informado");

                if (String.IsNullOrWhiteSpace(image.Name))
                    commandResult.Notifications.Add("Informe o nome do arquivo");

                if (String.IsNullOrWhiteSpace(image.Description))
                    commandResult.Notifications.Add("Informe a descrição do arquivo");

                if (image.Name.Length > 80)
                    commandResult.Notifications.Add("O tamanho do campo excedeu 80 caratecteres");

                if (image.Description.Length > 200)
                    commandResult.Notifications.Add("O tamanho do campo excedeu 200 caratecteres");
            }

            return commandResult;
        }

        public CommandResult ValidadeFileExtention(Data.Entities.Image image)
        {
            var commandResult = new CommandResult();

            if (image.FileExtention.Trim().ToUpper() != ".PNG")
                commandResult.Notifications.Add("O tipo de arquivo deve ser somente PNG");

            return commandResult;
        }

        public CommandResult ValidadeQuantityOfImages(ICollection<Data.Entities.Image> images)
        {
            var commandResult = new CommandResult();

            if (images.Count == 8)
                commandResult.Notifications.Add("A quantidade de imagens cadastradas não pode ultrapassar 8 registros");

            return commandResult;
        }
    }
}
