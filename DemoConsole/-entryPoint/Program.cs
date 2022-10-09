using DemoConsole = Trit.DemoConsole;

while (true)
{
    WriteLine();
    WriteLine("Pick a hex number: ");
    var picked = ReadKey().KeyChar;
    Clear();

    // All sources available
    // @ https://github.com/Team-Rockstars-IT/Trit.CsharpElevenDemo
    await (picked switch
    {
        '1' => DemoConsole._1_String_interpolation.Demo.Main(),
        '2' => DemoConsole._2_Pattern_matching.Demo.Main(),
        '3' => DemoConsole._3_File_local_types.Demo.Main(),
        '4' => DemoConsole._4_Raw_string_literals.Demo.Main(),
        '5' => DemoConsole._5_Method_group_conversion.Demo.Main(),
        '8' => DemoConsole._8_Generic_attributes.Demo.Main(),
        _ => Task.CompletedTask
    });

    if (picked == 'Q')
    {
        return 0;
    }

    ReadLine();
}