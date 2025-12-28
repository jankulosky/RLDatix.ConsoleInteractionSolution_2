using AllocateQ3_ConsoleInteraction.Common.Exceptions;
using AllocateQ3_ConsoleInteraction.Common.Validators;
using AllocateQ3_ConsoleInteraction.Interfaces;
using AllocateQ3_ConsoleInteraction.Services.Interfaces;
using System;
using System.Linq;

namespace AllocateQ3_ConsoleInteraction
{
    public class SongPlayer
    {
        private readonly ISongVerseFormatter _formatter;
        private readonly IAnimalService _animalService;
        private readonly ILocalizer _localizer;

        public SongPlayer(ISongVerseFormatter formatter, IAnimalService animalService, ILocalizer localizer)
        {
            _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
            _animalService = animalService ?? throw new ArgumentNullException(nameof(animalService));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
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
                    Console.WriteLine(_localizer.GetString("FirstInputQuestion"));
                    Console.WriteLine("1. " + _localizer.GetString("PlayAllAnimals"));
                    Console.WriteLine("2. " + _localizer.GetString("PlaySingleAnimal"));
                    Console.WriteLine("3. " + _localizer.GetString("AddNewAnimal"));
                    Console.WriteLine("4. " + _localizer.GetString("RemoveAnimal"));

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
                            Console.WriteLine(_localizer.GetString("InvalidInput"));
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

                Console.WriteLine(_localizer.GetString("StopApplication"));
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
                Console.WriteLine(_localizer.GetString("NoAnimalsAvailable"));
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

            Console.Write(_localizer.GetString("EnterAnimalType"));
            var type = Console.ReadLine();

            if (string.IsNullOrEmpty(type))
            {
                Console.WriteLine(_localizer.GetString("InvalidInput"));
                return;
            }

            var animal = _animalService.GetAnimal(type);

            if (animal == null)
            {
                Console.WriteLine(_localizer.GetString("AnimalNotFound"));
                return;
            }

            Play(animal.Type, animal.Sound);
        }

        private void AddAnimal()
        {
            Console.Write(_localizer.GetString("EnterAnimalType"));
            var type = Console.ReadLine().CheckNullOrEmpty();

            Console.Write(_localizer.GetString("EnterAnimalSound"));
            var sound = Console.ReadLine().CheckNullOrEmpty();

            _animalService.AddAnimal(type, sound);

            if (type != null && sound != null)
            {
                _animalService.AddAnimal(type, sound);
                Console.WriteLine(_localizer.GetString("AnimalAdded"));
            }
        }

        private void RemoveAnimal()
        {
            PrintAnimals();

            Console.Write(_localizer.GetString("EnterAnimalTypeToRemove"));
            var type = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(type))
            {
                _animalService.RemoveAnimal(type);
                Console.WriteLine(_localizer.GetString("AnimalRemoved"));
            }
        }

        private void PrintAnimals()
        {
            var animals = _animalService.GetAnimals();

            if (!animals.Any())
            {
                Console.WriteLine(_localizer.GetString("NoAnimalsAvailable"));
                return;
            }

            foreach (var animal in animals)
            {
                Console.WriteLine($"- {animal.Type}");
            }
        }
    }
}
