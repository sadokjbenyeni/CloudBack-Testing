import { Pipe, PipeTransform } from "@angular/core";
import { CardType } from "./models/CardType";

@Pipe({
    name: 'enumToArray'
})
export class EnumToArrayPipe implements PipeTransform {
    transform(data: Object) {
        return Object.keys(CardType);

    }
}