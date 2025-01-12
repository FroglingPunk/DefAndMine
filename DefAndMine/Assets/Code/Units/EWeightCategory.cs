public enum EWeightCategory
{
    // о Не получает урона при падении*
    // о Тонет в воде
    // о Ходит по любому льду
    // о Бонус к передвижению
    // о Минус к урону ближнего боя
    Lightweight,

    // о Получает базовый урон при падении
    // о Не тонет в воде, не может совершать действия кроме передвижения
    // о Ходит только по цельному льду, ломает треснувший лёд
    // о Без бонусов
    Medium,

    // о Получает повышенный урон при падении, отталкивает соседние ячейки, завершает путь и не движется дальше
    // о Не тонет в воде, может совершать действия
    // о Трескает цельный лёд, ломает треснувший
    // о Минус передвижение
    // о Бонус к урону ближнего боя
    Heavyweight
}