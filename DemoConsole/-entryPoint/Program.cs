using DemoConsole = Trit.DemoConsole;

while (true)
{
    Clear();
    WriteLine("Pick a hex number: ");
    var picked = ReadKey().KeyChar;
    Clear();

    // All sources available
    // @ https://github.com/Team-Rockstars-IT/Trit.CsharpElevenDemo
    await (picked switch
    {
        '1' => DemoConsole._1_Numbers.Demo.Main(),
        '2' => DemoConsole._2_Pattern_matching.Demo.Main(),
        '3' => DemoConsole._3_File_local_types.Demo.Main(),
        '4' => DemoConsole._4_Raw_string_literals.Demo.Main(),
        '5' => DemoConsole._5_Required_properties.Demo.Main(),
        '6' => DemoConsole._6_Ref_scoped_and_structs.Demo.Main(),
        '7' => DemoConsole._7_Nameof_and_method_groups.Demo.Main(),
        '8' => DemoConsole._8_Generic_attributes.Demo.Main(),
        '9' => DemoConsole._9_String_interpolation.Demo.Main(),
        'A' => DemoConsole.A_Utf8.Demo.Main(),
        'B' => DemoConsole.B_TextJson.Demo.Main(),
        _ => Task.CompletedTask
    });

    if (picked == 'Q')
    {
        return 0;
    }

    ReadLine();
}