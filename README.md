# BankProject
This project is a schoolproject where I simulate a bankapplication in a .NET Core Console application.


<h3>Struktur</h3>

Användarna är lagrade i en 2D string-array och bankkontona är lagrade i en lista med string-arrays.
Programmet utgår från att man börjar med att logga in. Den funktionen är en metod som kallas från en loop. Misslyckas man med att logga in får man vänta i 3 minuter innan loopen fortsätter och man får logga in igen. Lyckas man med att logga in hoppar man däremot in i en ny loop där metoden för menyn körs. Då får man upp flera val på vad man vill göra. Valen är :
<ul>
<li>1. Se dina konton och saldo.</li> 
<li>2. Överföring mellan konton.</li>
<li>3. Ta ut pengar.</li>
<li>4. Sätta in pengar.</li>
<li>5. Öppna nytt konto.</li>
<li>6. Logga ut.</li>
</ul>

Vid alla val körs en metod som gör det man vill utföra. Vid flera av metoderna behöver man vid något tillfälle skriva in sin PIN. Misslyckas man med det loggas man ut och blockas från att logga in på 3 minuter med den användaren. Så det fungerar är att dessa metoder returnerar false om användaren ska loggas ut. Metoden för menyn returnerar sedan vidare false om loopen med menyn ska brytas och att man ska hamna i loopen där man behöver logga in igen.


<h3>Reflektion</h3>
Jag valde att bygga upp det här på det här sättet för jag kände att det blev kortast och tydligast kod så. Att ha en loop för inloggen och en loop inuti för menyn som man hamnade i när man loggade in gjorde att flödet blev så jag ville ha det. Det gjorde det också väldigt enkelt att bygga in att man blev utloggad och blockad i tre minuter när man i någon av metoderna från menyn matat in fel PIN för många gånger. För jag byggde så att metoden för menyn bestod av en switch-case som körde den metoden för det man vill utföra t.ex. uttag av pengar. Då kunde jag bygga så att metoden för uttaget returnerad false om man skulle loggas ut och då returnerade meny-metoden i sin tur false och då hamnar man i loppen för login igen samtidigt som den användaren blockeras i 3 minuter. Returnerar meny-metoden true fortsätter den meny-loopen att köras och man får välja vad man vill göra igen. Det här sättet gjorde också att jag slapp massa If/else och själva koden i main blev väldigt kort. För min första tanke var att ha en lite längra main med flera if/else men jag insåg ganska snabbt att det skulle bli mycket smidigare och kortare med två loopar istället. 

En del jag övervägde lite olika alternativ hur jag skulle byggga det var hur det skulle fungera kring när en användare skulle bli blockad från att logga in och om det bara var en eller flera. Det jag kom fram till som jag tycker var mest logiskt var att om man inte var inloggad och skrev fel användare/lösenord så ska man inte kunna logga in på någon användare alls på 3 minuter. Medan om man var inloggad och skrev fel PIN vid t.ex. ett uttag så skulle bara den inloggade användaren bli blockerad från att logga in i 3 minuter.

En annan del jag övervägde en del hur jag skulle göra var kring lagringen av användare och bankkonton. Det absolut enklaste sättet hade ju varit att använda klasser och skapa objekt och lagra dem i listor. Men eftersom vi skulle använda arrayer så tyckte jag att det kunde fungera också. Jag valde att använda en 2D string-array till användarna eftersom användarna skulle vara lika många hela tiden. Jag valde string-arrays eftersom det blev enklast så och det gjorde inget att alla värden behöver vara strängar för det fungerar bra att konvertera dem. Det blev också ganska tydligt när en användare är en rad i 2D-arrayen. Jag valde en separat lista till bankkontona eftersom användarna skulle kunna öppna nya konton så det var smidigt då en lista är mer dynamisk. Jag kopplade ihop bankkontona till användarna genom att lagra kunddnumret som konto tillhörde. 
