using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1_Csharp_Courses
{
    class Program
    {
        static void Main(string[] args)
        {
            TextTemplates.ShowGreetingText();
            for (bool userStillWorkingFlag=true; userStillWorkingFlag; )
            {
                switch (TextTemplates.MainMenuInteract())
                {
                    case 1:
                        UserStorage.AddNewUser();
                        break;
                    case 2:
                        UserStorage.RedactUser();
                        break;
                    case 3:
                        UserStorage.DeleteUser();
                        break;
                    case 4:
                        UserStorage.ShowUsers();
                        break;
                    case 5:
                        TextTemplates.ShowAboutAutor();
                        break;
                    case 6:
                        userStillWorkingFlag = TextTemplates.IsUserEnding();
                        break;
                }
            }
            TextTemplates.ShowEndingText();
        }
    }

    public class TextTemplates
    {
        public static void ShowGreetingText()
        {
            string greetingMenuString = "Добро пожаловать в записную книжку!";
            greetingMenuString += "\nЭто консольное приложение является результатом моей работы над первой лабораторной работой с курса по языку C# в ИТМО";
            greetingMenuString += "\nЕсли у вас есть предложения по улучшению работы приложения, или вы нашли баг, просьба отправлять все сообщения\nна 27alpetr@gmail.com";
            greetingMenuString += "\nДля начала работы с приложением нажмите любую кнопку. Приятного использования!";
            Console.WriteLine(greetingMenuString);
            Console.ReadKey();
        }

        public static int MainMenuInteract()
        {
            Console.Clear();
            string mainMenuText = "-----------------------------------------------------------------------------------------------------------------------";
            mainMenuText += "\n\t\t\t\t\t        ГЛАВНОЕ МЕНЮ ПРОГРАММЫ";
            mainMenuText += "\n1)Создать новую запись\t\t\t\t\t\t\t\t4)Просмотреть существующие записи";
            mainMenuText += "\n2)Отредактировать существующую запись\t\t\t\t\t\t5)Об авторе";
            mainMenuText += "\n3)Удалить существующую запись\t\t\t\t\t\t\t6)Завершить работу";
            mainMenuText += "\n-----------------------------------------------------------------------------------------------------------------------\n";
            Console.Write(mainMenuText);
            return UserOption(6);
        }

        public static void ShowAboutAutor()
        {
            Console.Clear();
            string autorInfo = "Разработчик - Петров Александр Денисович, студент 1 курса факультета БИТ университета ИТМО.";
            autorInfo += "\nПрограмма разработана в качестве задания лабораторной работы №1 курса Александра Сергеевича Исаева \"NowYouSeeSharp\".";
            autorInfo += "\nКонтактный e-mail для всех пожеланий и баг-репортов: 27alpetr@gmail.com";
            autorInfo += "\nУниверситет ИТМО. 2019/2020 учебный год. Ни одни права не защищены, но мне, в общем-то, всё равно.";
            autorInfo += "\n\nДля выхода в главное меню нажмите любую клавишу";
            Console.WriteLine(autorInfo); Console.ReadLine();
        }
        public static bool IsUserEnding()
        {
            Console.Clear();
            string questionText = "Вы собираетесь прекратить работу с программой.";
            questionText += "\nПомните, что программа работает в режиме сессионного хранения информации, и после выхода все данные будут недоступны.";
            questionText += "\nВы точно хотите выйти из приложения? ";
            questionText += "\nВаши действия: 1 - Да; 2 - Вернуться в главное меню";
            Console.WriteLine(questionText);
            for (; ; )
            {
                int userAnswer = UserOption(2);
                if (userAnswer == 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }               
            }
        }

        public static int UserOption(int numberOfActions)
        {
            Console.Write("Действие: "); string userAnswer = Console.ReadLine();
            if ((!Int32.TryParse(userAnswer, out int result)) || (result < 1) || (result > numberOfActions))
            {
                Console.Write("Команда не распознана. Введите действие ещё раз: ");
                userAnswer = Console.ReadLine();
            }          
            while ((!Int32.TryParse(userAnswer, out result))||(result<1)||(result>numberOfActions))
            {
                Console.Write("Команда по-прежнему не распознана. Введите действие ещё раз: ");
                userAnswer = Console.ReadLine();
            }
            return Int32.Parse(userAnswer);
        }
        public static int UserOptionWithEnd(int numberOfActions)
        {
            Console.Write("Действие: "); string userAnswer = Console.ReadLine();
            if (userAnswer.Trim() == "end") return 0;
            if ((!Int32.TryParse(userAnswer, out int result)) || (result < 1) || (result > numberOfActions))
            {
                Console.Write("Команда не распознана. Введите действие ещё раз: ");
                userAnswer = Console.ReadLine(); if (userAnswer.Trim() == "end") return 0;
            }
            while ((!Int32.TryParse(userAnswer, out result)) || (result < 1) || (result > numberOfActions))
            {
                Console.Write("Команда по-прежнему не распознана. Введите действие ещё раз: ");
                userAnswer = Console.ReadLine(); if (userAnswer.Trim() == "end") return 0;
            }
            return Int32.Parse(userAnswer);
        }

        public static void ShowEndingText()
        {
            Console.Clear();
            string endingText = "Спасибо за испольование моей программы! " +
            "\nНапоминаю вам, что контактный e-mail для всех пожеланий и баг-репортов: 27alpetr@gmail.com"
            + "\nНажмите любую клавишу, чтобы выйти. ";
            Console.Write(endingText); Console.ReadLine();
        }
    }

    public class UserStorage
    {
        static List<User> allUsers = new List<User>();
        public static void AddNewUser()
        {
            Console.Clear();
            string headerText = "-----------------------------------------------------------------------------------------------------------------------";
            headerText += "\nРежим добавления новой записи\n";
            headerText += "-----------------------------------------------------------------------------------------------------------------------";
            headerText += "\nВведите информацию о новой записи. Поля, отмеченные звёздочкой (*), являются обязательными к заполнению.";
            Console.WriteLine(headerText);
            string[] inputData = new string[9] { "", "", "", "", "", "", "", "", "", };
            while (inputData[0].Trim() == "")
            {
                Console.Write("\nФамилия*: "); inputData[0] = Console.ReadLine();
                if(inputData[0].Trim() == "")
                {
                    Console.Write("Поле \"Фамилия\" не может быть пустым. Введите его ещё раз.");
                }
            }
            while (inputData[1].Trim() == "")
            {
                Console.Write("\nИмя*: "); inputData[1] = Console.ReadLine();
                if (inputData[1].Trim() == "")
                {
                    Console.Write("Поле \"Имя\" не может быть пустым. Введите его ещё раз.");
                }
            }
            Console.Write("\nОтчество: "); inputData[2] = Console.ReadLine();
            while (inputData[3].Trim() == "")
            {
                Console.Write("\nНомер телефона (только цифры)*: "); inputData[3] = Console.ReadLine();
                if (inputData[3].Trim() == "")
                {
                    Console.Write("Поле \"Номер телефона\" не может быть пустым. Введите его ещё раз.");
                }
                else if (!long.TryParse(inputData[3], out long z))
                {
                    Console.Write("В поле \"Номер телефона\" могут содержаться только цифры. Введите его ещё раз.");
                    inputData[3] = "";
                }
            }
            while (inputData[4].Trim() == "")
            {
                Console.Write("\nСтрана*: "); inputData[4] = Console.ReadLine();
                if (inputData[4].Trim() == "")
                {
                    Console.Write("Поле \"Страна\" не может быть пустым. Введите его ещё раз. ");
                }
            }
            Console.Write("\nДата рождения: "); inputData[5] = Console.ReadLine();
            if (inputData[5].Trim() != "")
            {
                while((inputData[5].Trim()!="")&&(!DateTime.TryParse(inputData[5], out DateTime z)))
                {
                    Console.WriteLine("Дата записана в неверном формате. Запишите дату через точку в соответствии с форматом, установленным в вашей системе.");
                    Console.Write("Например, 27.01.1970 . Вы по-прежнему можете оставить это поле пустым.");
                    Console.Write("\nДата рождения:"); inputData[5] = Console.ReadLine();
                }
            }
            Console.Write("\nОрганизация: "); inputData[6] = Console.ReadLine();
            Console.Write("\nДолжность: "); inputData[7] = Console.ReadLine();
            Console.Write("\nПрочие заметки: "); inputData[8] = Console.ReadLine();
            allUsers.Add(new User(inputData));
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            Console.Write("\nЗапись создана! Нажмите любую клавишу, чтобы продолжить.");
            Console.ReadLine();
        }

        public static void RedactUser()
        {
            Console.Clear();
            string headerText = "-----------------------------------------------------------------------------------------------------------------------";
            headerText += "\nРежим изменения существующей записи\n";
            headerText += "-----------------------------------------------------------------------------------------------------------------------";
            Console.WriteLine(headerText);
            foreach (var user in allUsers)
            {
                Console.WriteLine($"{allUsers.IndexOf(user)+1}) {user.Surname} {user.Name}, {user.MobilePhone}");
            }
            if (allUsers.Count == 0) 
            { 
                Console.Write("Упс, кажется, список записей пуст! Редактировать вам нечего. Нажмите любую клавишу, чтобы выйти в главное меню.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Введите номер нужной записи из списка или \"end\", чтобы отменить редактирование.");
            for (bool userRedacted = false; !userRedacted;)
            {
                int redactIndex = TextTemplates.UserOptionWithEnd(allUsers.Count) - 1;
                if (redactIndex == -1)
                {
                    Console.WriteLine("Редактирование записей прервано. Нажмите любую клавишу для выхода в главное меню.");
                    Console.ReadLine();
                    break;
                }
                userRedacted = true;
                User currentUser = allUsers[redactIndex];
                Console.Clear();
                Console.WriteLine(currentUser.ToString());
                Console.WriteLine("\nВведите номер секции для редактирования.");
                currentUser.RedactInfo(TextTemplates.UserOption(9));
                while (true)
                {
                    Console.WriteLine("Запись отредактирована! Вы можете продолжить редактирование этой записи, указав нужный номер ещё раз, или выйти. \nДля выхода введите \"end\".");
                    int answer = TextTemplates.UserOptionWithEnd(9);
                    if (answer == 0) break;
                    else currentUser.RedactInfo(answer);
                }
            }
        }
        public static void DeleteUser()
        {
            Console.Clear();
            string headerText = "-----------------------------------------------------------------------------------------------------------------------";
            headerText += "\nРежим удаления существующей записи\n";
            headerText += "-----------------------------------------------------------------------------------------------------------------------";
            Console.WriteLine(headerText);
            foreach (var user in allUsers)
            {
                Console.WriteLine($"{allUsers.IndexOf(user)+1}) {user.Surname} {user.Name}, {user.MobilePhone}");
            }
            if (allUsers.Count == 0)
            {
                Console.Write("Упс, кажется, список записей пуст! Удалять вам нечего. Нажмите любую клавишу, чтобы выйти в главное меню.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Введите номер нужной записи из списка или \"end\", чтобы отменить удаление.");
            for (bool userDeleted = false; !userDeleted;)
            {
                int deleteIndex = TextTemplates.UserOptionWithEnd(allUsers.Count) - 1;
                if (deleteIndex == -1)
                {
                    Console.WriteLine("Удаление записей прервано. Нажмите любую клавишу для выхода в главное меню.");
                    Console.ReadLine();
                    break;
                }
                Console.WriteLine($"Вы точно хотите удалить запись номер {deleteIndex+1} ({allUsers[deleteIndex].Name} {allUsers[deleteIndex].Surname})?\n1)Да\t\t\t\t2)Нет");
                int desicion = TextTemplates.UserOption(2);
                if (desicion == 1)
                {
                    allUsers.RemoveAt(deleteIndex);
                    Console.WriteLine("Запись удалена! Для продолжения нажмите любую клавишу.");
                    Console.ReadLine();
                    userDeleted = true;
                }
                else
                {
                    Console.WriteLine("Удаление записи отменено. Вы можете удалить другую запись или выйти в главное меню.\nВведите номер нужной записи или \"end\", чтобы закончить удаление.");                  
                }
            }
        }
        
        public static void ShowUsers()
        {
            Console.Clear();
            string headerText = "-----------------------------------------------------------------------------------------------------------------------";
            headerText += "\nРежим просмотра существующих записей\n";
            headerText += "-----------------------------------------------------------------------------------------------------------------------";
            Console.WriteLine(headerText);
            foreach (var user in allUsers)
            {
                Console.WriteLine($"{allUsers.IndexOf(user) + 1}) {user.Surname} {user.Name}, {user.MobilePhone}");
            }
            if (allUsers.Count == 0)
            {
                Console.Write("Упс, кажется, список записей пуст! Смотреть вам нечего. Нажмите любую клавишу, чтобы выйти в главное меню.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Для более детального просмотра информации, введите номер нужной записи или \"end\", чтобы закончить просмотр.");
            while (true)
            {
                int decision = TextTemplates.UserOptionWithEnd(allUsers.Count);
                if (decision == 0) break;
                else Console.WriteLine(allUsers[decision-1].ToString());
                Console.WriteLine("Вы можете продолжить просмотр информации, введя номер нужной записи или \"end\", чтобы закончить просмотр.");
            }
        }

    }

    public class User
    {
        public string Surname { get; private set; }
        public string Name { get; private set; }
        public string SecondName { get; private set; }
        public string MobilePhone { get; private set; }
        public string Country { get; private  set; }
        public string DateOfBirth { get; private set; }
        public string Organization { get; private set; }
        public string Appointment { get; private set; }
        public string OtherNotes { get; private set; }

        public User(string[] inputData)
        {
            Surname = inputData[0];
            Name = inputData[1];
            SecondName = inputData[2]; if (inputData[2].Trim() == "") SecondName = "Не указано";
            MobilePhone = inputData[3];
            Country = inputData[4];
            DateOfBirth = inputData[5]; if (inputData[5].Trim() == "") DateOfBirth = "Не указана";
            Organization = inputData[6]; if (inputData[6].Trim() == "") Organization = "Не указана";
            Appointment = inputData[7]; if (inputData[7].Trim() == "") Appointment = "Не указана";
            OtherNotes = inputData[8]; if (inputData[8].Trim() == "") OtherNotes = "Заметок нет";
        }

        public override string ToString()
        {
            string infoText ="-----------------------------------------------------------------------------------------------------------------------";
            infoText += "\nИнформация о существующей записи";
            infoText = "\n-----------------------------------------------------------------------------------------------------------------------";
            infoText += $"\n1)Фамилия: {Surname}\n2)Имя: {Name}\n3)Отчество: {SecondName}\n4)Номер телефона: {MobilePhone}\n5)Страна: {Country}";
            infoText += $"\n6)Дата рождения: {DateOfBirth}\n7)Организация: {Organization}\n8)Должность {Appointment}\n9)Прочие заметки: {OtherNotes} ";
            infoText += "\n-----------------------------------------------------------------------------------------------------------------------";
            return infoText;
        }
        public void RedactInfo(int inputNumber)
        {
            switch (inputNumber)
            {
                case 1:
                    Surname = "";
                    while (Surname.Trim() == "")
                    {
                        Console.Write("\nФамилия*: "); Surname = Console.ReadLine();
                        if (Surname.Trim() == "")
                        {
                            Console.Write("Поле \"Фамилия\" не может быть пустым. Введите его ещё раз.");
                        }
                    }
                    break;
                case 2:
                    Name = "";
                    while (Name.Trim() == "")
                    {
                        Console.Write("\nИмя*: "); Name = Console.ReadLine();
                        if (Name.Trim() == "")
                        {
                            Console.Write("Поле \"Имя\" не может быть пустым. Введите его ещё раз.");
                        }
                    }
                    break;
                case 3:
                    Console.Write("Отчество: ");
                    SecondName = Console.ReadLine(); if (SecondName.Trim() == "") SecondName = "Не указано";
                    break;
                case 4:
                    MobilePhone = "";
                    while (MobilePhone.Trim() == "")
                    {
                        Console.Write("\nНомер телефона (только цифры)*: "); MobilePhone = Console.ReadLine();
                        if (MobilePhone.Trim() == "")
                        {
                            Console.Write("Поле \"Номер телефона\" не может быть пустым. Введите его ещё раз.");
                        }
                        else if (!long.TryParse(MobilePhone, out long z))
                        {
                            Console.Write("В поле \"Номер телефона\" могут содержаться только цифры. Введите его ещё раз.");
                            MobilePhone = "";
                        }
                    }
                    break;
                case 5:
                    Country = "";
                    while (Country.Trim() == "")
                    {
                        Console.Write("\nСтрана*: "); Country = Console.ReadLine();
                        if (Country.Trim() == "")
                        {
                            Console.Write("Поле \"Страна\" не может быть пустым. Введите его ещё раз. ");
                        }
                    }
                    break;
                case 6:
                    DateOfBirth = "";
                    Console.Write("\nДата рождения: "); DateOfBirth = Console.ReadLine();
                    if (DateOfBirth.Trim() != "")
                    {
                        while ((DateOfBirth.Trim() != "") && (!DateTime.TryParse(DateOfBirth, out DateTime z)))
                        {
                            Console.WriteLine("Дата записана в неверном формате. Запишите дату через точку в соответствии с форматом, установленным в вашей системе.");
                            Console.Write("Например, 27.01.1970 . Вы по-прежнему можете оставить это поле пустым.");
                            Console.Write("\nДата рождения:"); DateOfBirth = Console.ReadLine();
                        }
                    }
                    if (DateOfBirth.Trim() == "") DateOfBirth = "Не указана";
                    break;
                case 7:
                    Console.Write("Организация: ");
                    Organization = Console.ReadLine(); if (Organization.Trim() == "") Organization = "Не указана";
                    break;
                case 8:
                    Console.Write("Должность: ");
                    Appointment = Console.ReadLine(); if (Appointment.Trim() == "") Appointment = "Не указана";
                    break;
                case 9:
                    Console.Write("Прочие заметки: "); if (OtherNotes.Trim() == "") OtherNotes = "Заметок нет";
                    OtherNotes = Console.ReadLine();
                    break;
            }
        }
    }
}
