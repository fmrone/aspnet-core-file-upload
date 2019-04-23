using FMR.Image.Business.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMR.Image.Business
{
    public interface IImageBusiness
    {
        CommandResult Create(Data.Entities.Image image);
        CommandResult Delete(string id);
        Data.Entities.Image FindById(string id);
        IEnumerable<Data.Entities.Image> FindAll();

        CommandResult ValidateFields(Data.Entities.Image image);
        CommandResult ValidadeFileExtention(Data.Entities.Image image);
        CommandResult ValidadeQuantityOfImages(ICollection<Data.Entities.Image> images);
    }
}
