using AllocateQ3_ConsoleInteraction;
using AllocateQ3_ConsoleInteraction.Common;
using AllocateQ3_ConsoleInteraction.Common.Interfaces;
using AllocateQ3_ConsoleInteraction.Helpers;
using AllocateQ3_ConsoleInteraction.Interfaces;
using AllocateQ3_ConsoleInteraction.Repositories;
using AllocateQ3_ConsoleInteraction.Repositories.Interfaces;
using AllocateQ3_ConsoleInteraction.Services;
using AllocateQ3_ConsoleInteraction.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

var culture = CultureResolver.GetCurrentLanguage();
var services = new ServiceCollection();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

services.AddSingleton<IFilePathProvider>(_ => new FilePathProvider(configuration["AnimalsFilePath"]));
services.AddScoped(typeof(IFileStorage<>), typeof(FileStorage<>));

services.AddSingleton<ILocalizer>(_ => new ResourceLocalizer(culture));

services.AddScoped<IAnimalService, AnimalService>();
services.AddScoped<IAnimalRepository, AnimalRepository>();

services.AddSingleton<ISongVerseFormatter, OldMcDonaldVerseFormatter>();
services.AddSingleton<SongPlayer>();

using var provider = services.BuildServiceProvider();

var app = provider.GetRequiredService<SongPlayer>();

app.Run();