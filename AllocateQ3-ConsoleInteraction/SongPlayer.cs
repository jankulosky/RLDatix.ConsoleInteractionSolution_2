using AllocateQ3_ConsoleInteraction.Common.Exceptions;
using AllocateQ3_ConsoleInteraction.Common.Validators;
using AllocateQ3_ConsoleInteraction.Helpers;
using AllocateQ3_ConsoleInteraction.Interfaces;
using AllocateQ3_ConsoleInteraction.Services.Interfaces;
using System;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace AllocateQ3_ConsoleInteraction
{
    public class SongPlayer
    {
        private readonly ISongVerseFormatter _formatter;
        private readonly IAnimalService _animalService;
        private readonly (ResourceManager ResourceManager, CultureInfo Culture) _languagePack;

        public SongPlayer(ISongVerseFormatter formatter, IAnimalService animalService, (ResourceManager, CultureInfo) languagePack)
        {
            _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
            _animalService = animalService ?? throw new ArgumentNullException(nameof(animalService));
            _languagePack = languagePack;
        }

        public void Run()
        {
            string proceed;

            do
            {
                try
                {
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("-------       Song Player      --------");
                    Console.WriteLine("-----             for             -----");
                    Console.WriteLine("------ Old MacDonald Had a Farm -------");
                    Console.WriteLine("---------------------------------------\n");
                    Console.WriteLine(LocalizationHelper.GetString(_languagePack, "FirstInputQuestion"));
                    Console.WriteLine("1. " + LocalizationHelper.GetString(_languagePack, "PlayAllAnimals"));
                    Console.WriteLine("2. " + LocalizationHelper.GetString(_languagePack, "PlaySingleAnimal"));
                    Console.WriteLine("3. " + LocalizationHelper.GetString(_languagePack, "AddNewAnimal"));
                    Console.WriteLine("4. " + LocalizationHelper.GetString(_languagePack, "RemoveAnimal"));

                    var input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            PlayAllAnimals();
                            break;

                        case "2":
                            PlaySingleAnimal();
                            break;

                        case "3":
                            AddAnimal();
                            break;

                        case "4":
                            RemoveAnimal();
                            break;

                        default:
                            Console.WriteLine(LocalizationHelper.GetString(_languagePack, "InvalidInput"));
                            break;
                    }
                }
                catch (InputException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error occurred: {ex.Message}");
                }

                Console.WriteLine(LocalizationHelper.GetString(_languagePack, "StopApplication"));
                proceed = Console.ReadLine().Trim().ToLower();
                Console.Clear();
            } while (proceed != "no");
        }

        private void Play(string type, string sound)
        {
            foreach (var line in _formatter.FormatSongVerse(type, sound))
            {
                Console.WriteLine(line);
            }

            Console.WriteLine();
        }

        private void PlayAllAnimals()
        {
            var animals = _animalService.GetAnimals();

            if (!animals.Any())
            {
                Console.WriteLine(LocalizationHelper.GetString(_languagePack, "NoAnimalsAvailable"));
                return;
            }

            foreach (var animal in animals)
            {
                Play(animal.Type, animal.Sound);
            }
        }

        private void PlaySingleAnimal()
        {
            PrintAnimals();

            Console.Write(LocalizationHelper.GetString(_languagePack, "EnterAnimalType"));
            var type = Console.ReadLine();

            if (string.IsNullOrEmpty(type))
            {
                Console.WriteLine(LocalizationHelper.GetString(_languagePack, "InvalidInput"));
                return;
            }

            var animal = _animalService.GetAnimal(type);

            if (animal == null)
            {
                Console.WriteLine(LocalizationHelper.GetString(_languagePack, "AnimalNotFound"));
                return;
            }

            Play(animal.Type, animal.Sound);
        }

        private void AddAnimal()
        {
            Console.Write(LocalizationHelper.GetString(_languagePack, "EnterAnimalType"));
            var type = Console.ReadLine().CheckNullOrEmpty();

            Console.Write(LocalizationHelper.GetString(_languagePack, "EnterAnimalSound"));
            var sound = Console.ReadLine().CheckNullOrEmpty();

            _animalService.AddAnimal(type, sound);

            if (type != null && sound != null)
            {
                _animalService.AddAnimal(type, sound);
                Console.WriteLine(LocalizationHelper.GetString(_languagePack, "AnimalAdded"));
            }
        }

        private void RemoveAnimal()
        {
            PrintAnimals();

            Console.Write(LocalizationHelper.GetString(_languagePack, "EnterAnimalTypeToRemove"));
            var type = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(type))
            {
                _animalService.RemoveAnimal(type);
                Console.WriteLine(LocalizationHelper.GetString(_languagePack, "AnimalRemoved"));
            }
        }

        private void PrintAnimals()
        {
            var animals = _animalService.GetAnimals();

            if (!animals.Any())
            {
                Console.WriteLine(LocalizationHelper.GetString(_languagePack, "NoAnimalsAvailable"));
                return;
            }

            foreach (var animal in animals)
            {
                Console.WriteLine($"- {animal.Type}");
            }
        }
    }
}
