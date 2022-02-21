using System;

// конкретні декоратори
public class SpecialLightingDecorator : Decorator
{
    public string specialLighting;

    //  do constructors
     
     // do override

/*
    public override void GoToVet()
    {
        if (this.croppedEars && this.croppedTail)
        {
            Console.WriteLine("Hunting dog is already ready for the first hunting session");
        }
        else
        {
            if (this.croppedTail == false)
            {
                Console.WriteLine("Get tail cropping");
                this.croppedTail = true;
            }
            if (this.croppedEars == false)
            {
                Console.WriteLine("Get ears cropping");
                this.croppedEars = true;
            }
        }

        base.GoToVet();
    }
*/

}

public class SpecialSoundInstallationDecorator : Decorator
{

}