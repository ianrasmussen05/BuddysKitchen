import { MealType } from "../types/enums";

export function getMealType(type: MealType) {
    return MealType[type];
};
