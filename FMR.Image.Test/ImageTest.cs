using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FMR.Image.Test
{
    [TestClass]
    public class ImageTest
    {
        private readonly ICollection<Data.Entities.Image> _mockImages;
        private readonly Data.Entities.Image _mockImage;

        private Business.ImageBusiness _imageBusiness;

        public ImageTest()
        {
            _mockImages = new List<Data.Entities.Image>();
            _mockImage = new Data.Entities.Image();

            _imageBusiness = new Business.ImageBusiness();
        }

        [TestMethod]
        public void Success_To_File_With_Png_Extention()
        {
            _mockImage.FileExtention = ".png";
            
            var expected = _imageBusiness.ValidadeFileExtention(_mockImage).Success;
            var actual = true;

            Assert.AreEqual(expected, actual, "Success_To_File_With_Png_Extention");
        }

        [TestMethod]
        public void Error_When_Limit_Of_Images_Exceed()
        {
            for (int i = 0; i < 8; i++)
            {
                _mockImages.Add(new Data.Entities.Image { Name = string.Format("Image #{0}", i + 1) });
            }

            var expected = _imageBusiness.ValidadeQuantityOfImages(_mockImages).Success;
            var actual = false;

            Assert.AreEqual(expected, actual, "Error_When_Limit_Of_Images_Exceed");
        }
    }
}
