using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;


// Config variables
const string TESESSERACT_TRAINING_DATA_FOLDER = @"../../../resources/tessdata/";
const string TEST_IMAGES_FOLDER = @"../../../test_images/";

// Loading ocr
Tesseract OCR = LoadTesseract();

// Fichero de prueba
string testImageFilename = "crema.jpg";

// Loading image
Image<Bgr, byte> image = LoadImage(testImageFilename);
Image<Gray, byte> imageGray = image.Convert<Gray, byte>();

// Procesamos la imagen para obtener mejores resultados
imageGray = GetBinarizedImageOtsu(imageGray);
//imageGray = ApplyGaussianFilter(imageGray); 
//imageGray = EqualizeHistogram(imageGray);

// Extracting text
GetTextFromImageGray(OCR, imageGray);

// Display images
DisplayImageGray(imageGray);









// Tesseract
static string GetTextFromImageGray(Tesseract ocr, Image<Gray, byte> image)
{
    ocr.SetImage(image);
    ocr.Recognize();

    string texto = ocr.GetUTF8Text();
    Console.WriteLine(texto);

    return texto;
}

static string GetTextFromImageBgr(Tesseract ocr, Image<Bgr, byte> image)
{
    ocr.SetImage(image);
    ocr.Recognize();

    string texto = ocr.GetUTF8Text();
    Console.WriteLine(texto);

    return texto;
}
static Tesseract LoadTesseract()
{
    Tesseract ocr = new Tesseract(TESESSERACT_TRAINING_DATA_FOLDER, "eng", OcrEngineMode.TesseractOnly);
    return ocr;
}



// Loading images
static Image<Bgr, byte> LoadImage(string imgFilename)
{ 
    return new Image<Bgr, byte>(String.Format(@"{0}{1}", TEST_IMAGES_FOLDER, imgFilename));
}

//  Processing images
static Image<Bgr, byte> ResizeImage(Image<Bgr, byte> image, float scaleFactor)
{
    System.Drawing.Size newSize = new System.Drawing.Size((int)Math.Round(image.Width * scaleFactor), (int)Math.Round(image.Height * scaleFactor));
    Image<Bgr, byte> resizedImage = new Image<Bgr, byte>(newSize);
    CvInvoke.Resize(image, resizedImage, newSize);

    return resizedImage;
}

static Image<Gray, byte> EqualizeHistogram(Image<Gray, byte> image)
{
    // Ecualizar el histograma
    image._EqualizeHist();
    return image;
}

static Image<Gray, byte> GetBinarizedImageOtsu(Image<Gray, byte> image)
{
    double otsuThreshold = CvInvoke.Threshold(image, image, 0, 255, Emgu.CV.CvEnum.ThresholdType.Otsu);

    Image<Gray, byte> otsuImg = image.ThresholdBinary(new Gray(otsuThreshold), new Gray(255));

    return otsuImg;
}

static Image<Gray, byte> GetBinarizedImageThreshold(Image<Gray, byte> image, int threholdValue)
{
    Image<Gray, byte> thImg = image.ThresholdBinary(new Gray(threholdValue), new Gray(255));

    return thImg;
}

static Image<Gray, byte> ApplyGaussianFilter(Image<Gray, byte> image)
{
    var gaussianImg = image.Clone();
    System.Drawing.Size kernelSize = new System.Drawing.Size(3, 3);
    CvInvoke.GaussianBlur(image, gaussianImg, kernelSize, 0);

    return gaussianImg;
}


// Displaying images
static void DisplayImageBgr(Image<Bgr, byte> image)
{
    CvInvoke.Imshow("Image", image);
    CvInvoke.WaitKey(0);
}

static void DisplayImageGray(Image<Gray, byte> image)
{
    CvInvoke.Imshow("Image", image);
    CvInvoke.WaitKey(0);
}
