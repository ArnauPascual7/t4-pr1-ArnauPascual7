# t4-pr1-ArnauPascual7

### Arnau Pascual

Practice Statment -> [T4. PR1. Pràctica 1](https://docs.google.com/document/d/14zwFBoM9mjmuGa32U_vSWfnLPBEoeY0l2J4p7b26NWg/edit?pli=1&tab=t.0#heading=h.7cha7h76kgkf)

Documentació Entregable -> [T4. PR1. Pràctica 1 - Documentació](https://docs.google.com/document/d/1NPaLl9AqAp1485Ls8cZctHeaNeEEKVmNIq0QcSHRl2g/edit?tab=t.0#heading=h.7cha7h76kgkf)

## Solució del Sistema

La Solució està formada per dos projectes, el projecte principal de [RazorPages](EcoEnergyRazorPagesSolution/EcoEnergyRazorPages) i el projecte de [xUnitTesting](EcoEnergyRazorPagesSolution/xUnitTestProject). Dins del projecte de xUnitTesting només hi ha un arxiu UnitTest1.cs amb els tests unitàris. Dins del Projecte principal tenim, 3 carpetes noves que no venen amb el projecte de RAzor Pages de forma predeterminada.

[Model](EcoEnergyRazorPagesSolution/EcoEnergyRazorPages/Model): Aquest directori conté les calsses per a poder llegir i escriure en els arxius a més de la Classe WaterCosumptionComparer, per a les estadísitques de WaterCosnumption, i l'herència de EnergySystem per a poder afegir Sistema d'energia. A més d'aquestes classes també hi ha un [directori](EcoEnergyRazorPagesSolution/EcoEnergyRazorPages/Model/ClassDiagram) on es troba el diagrama de classes generat per Visual Studio.

[ModelData](EcoEnergyRazorPagesSolution/EcoEnergyRazorPages/ModelData): Aquest directori conté els arxius CSV, XML i JSON que s'utilitzen en el projecte.

[Tools](EcoEnergyRazorPagesSolution/EcoEnergyRazorPages/Tools): Aquest directori conté dues helper classes amb funcionalitats per a ajudar en el maneig de fitxer, llegir i escriure, i les funcionalitats per a poder fer les estadísitiques als consums d'aigua.

De forma predeterminada hi ha el directori [Pages](EcoEnergyRazorPagesSolution/EcoEnergyRazorPages/Pages), aquest conté totes les pàgina de la web. Dins d'aquest també hi ha les pàgines creades per mí sobre Simulacions, Consums d'aigua i Indicadors energètics, a més de les seves pàgines corresponents per a afegir registres.

## [Testing de la solució](EcoEnergyRazorPagesSolution/xUnitTestProject)

## Bibliografia

> Raquel Alemán i Eduard Ruesga. (Sense data). T4. Llibreries, fitxers, excepcions i col·leccions. GoogleDocs. Recuperat el 26/2/2025 de https://docs.google.com/document/d/18hNHYMmwVJkUn1tgyOlvartjc6FWHz15tikH8xaVDIg/edit?tab=t.0#heading=h.3uprr6i1f16a.

> Sebastián Miranda. (9/6/2021). enviar una variable por post a una funcion en c# asp.net. StackOverflow. Recuperat el 1/3/2025 de https://es.stackoverflow.com/questions/467887/enviar-una-variable-por-post-a-una-funcion-en-c-asp-net.

> Arash. (19/12/2020). How can I get a count of the total number of digits in a number?. StackOverflow. Recuperat el 1/3/2025 de https://stackoverflow.com/questions/4483886/how-can-i-get-a-count-of-the-total-number-of-digits-in-a-number.

> Sense Autor. (Sense data). List<T> Clase. Microsoft. Recuperat el 2/3/2025 de https://learn.microsoft.com/es-es/dotnet/api/system.collections.generic.list-1?view=net-8.0.

> Sense Autor. (Sense data). group (Cláusula, Referencia de C#). Microsoft. Recuperat el 2/3/2025 de https://learn.microsoft.com/es-es/dotnet/csharp/language-reference/keywords/group-clause.

> Sense Autor. (Sense data). List<T>.RemoveRange(Int32, Int32) Método. Microsoft. Recuperat el 2/3/2025 de https://learn.microsoft.com/es-es/dotnet/api/system.collections.generic.list-1.removerange?view=net-8.0#system-collections-generic-list-1-removerange(system-int32-system-int32).

> Jack Tyler. (8/3/2018). How to get values from anonymous list to a dictionary? duplicate. StackOverflow. Recuperat el 2/3/2025 de https://stackoverflow.com/questions/49174395/how-to-get-values-from-anonymous-list-to-a-dictionary.

> Arad. (1/11/2021). DateTime.Now equivalent for TimeOnly and DateOnly?. StackOverflow. Recuperat el 8/3/2025 de https://stackoverflow.com/questions/69798302/datetime-now-equivalent-for-timeonly-and-dateonly.

> Sense Autor. (Sense data). Get started with Bootstrap. GetBootstrap. Recuperat el 9/3/2025 de https://getbootstrap.com/docs/5.3/getting-started/introduction/.

> Matt. (30/3/2022). Call Model function from javascript in razor page. StackOverflow. Recuperat el 11/3/2025 de https://stackoverflow.com/questions/71680811/call-model-function-from-javascript-in-razor-page.

> Sense Autor. (Ultima actualització 10/5/2023). Model Binding. Learn Razor Pages. Recuperat el 26/1/2025 de https://www.learnrazorpages.com/razor-pages/model-binding.
