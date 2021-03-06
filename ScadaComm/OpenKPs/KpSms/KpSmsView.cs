﻿/*
 * Copyright 2016 Mikhail Shiryaev
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * 
 * Product  : Rapid SCADA
 * Module   : KpSms
 * Summary  : Device library user interface
 * 
 * Author   : Mikhail Shiryaev
 * Created  : 2009
 * Modified : 2016
 */

using Scada.Comm.Devices.AddressBook;
using Scada.Data;
using System.Collections.Generic;

namespace Scada.Comm.Devices
{
    /// <summary>
    /// Device library user interface
    /// <para>Пользовательский интерфейс библиотеки КП</para>
    /// </summary>
    public sealed class KpSmsView : KPView
    {
        /// <summary>
        /// Конструктор для общей настройки библиотеки КП
        /// </summary>
        public KpSmsView()
            : this(0)
        {
        }

        /// <summary>
        /// Конструктор для настройки конкретного КП
        /// </summary>
        public KpSmsView(int number)
            : base(number)
        {
            CanShowProps = true;
        }


        /// <summary>
        /// Описание библиотеки КП
        /// </summary>
        public override string KPDescr
        {
            get
            {
                return Localization.UseRussian ? 
                    "Отправка и приём SMS с использованием AT-команд.\n\n" +
                    "Команды ТУ:\n" +
                    "1 (бинарная) - отправка SMS;\n" +
                    "2 (бинарная) - произвольная AT-команда." :

                    "Sending and receiving SMS messages using AT commands.\n\n" +
                    "Commands:\n" +
                    "1 (binary) - send SMS message;\n" +
                    "2 (binary) - custom AT command.";
            }
        }

        /// <summary>
        /// Получить прототипы каналов КП по умолчанию
        /// </summary>
        public override KPCnlPrototypes DefaultCnls
        {
            get
            {
                KPCnlPrototypes prototypes = new KPCnlPrototypes();
                List<InCnlPrototype> inCnls = prototypes.InCnls;
                List<CtrlCnlPrototype> ctrlCnls = prototypes.CtrlCnls;

                // создание прототипов входных каналов
                inCnls.Add(new InCnlPrototype(Localization.UseRussian ? "Связь" : "Connection", BaseValues.CnlTypes.TS)
                {
                    Signal = 1,
                    ParamName = Localization.UseRussian ? "Связь" : "Connection",
                    ShowNumber = false,
                    UnitName = Localization.UseRussian ? "Нет - Есть" : "No - Yes",
                    EvEnabled = true,
                    EvOnChange = true
                });

                inCnls.Add(new InCnlPrototype(Localization.UseRussian ? "Кол-во событий" : "Event count",
                    BaseValues.CnlTypes.TI)
                {
                    Signal = 2,
                    ParamName = Localization.UseRussian ? "Событие" : "Event",
                    DecDigits = 0,
                    UnitName = Localization.UseRussian ? "Шт." : "pcs."
                });

                // создание прототипов каналов управления
                ctrlCnls.Add(new CtrlCnlPrototype(Localization.UseRussian ? "Отправка SMS" : "Send SMS message",
                    BaseValues.CmdTypes.Binary) { CmdNum = 1 });

                ctrlCnls.Add(new CtrlCnlPrototype(Localization.UseRussian ? "AT-команда" : "AT command",
                    BaseValues.CmdTypes.Binary) { CmdNum = 2 });

                return prototypes;
            }
        }

        /// <summary>
        /// Получить параметры опроса КП по умолчанию
        /// </summary>
        public override KPReqParams DefaultReqParams
        {
            get
            {
                return new KPReqParams() { Timeout = 5000, Delay = 500 };
            }
        }

        /// <summary>
        /// Отобразить свойства КП
        /// </summary>
        public override void ShowProps()
        {
            // отображение адресной книги
            FrmAddressBook.ShowDialog(AppDirs);
        }
    }
}