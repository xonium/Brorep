import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'urlPrettifier'})
export class UrlPrettifierPipe implements PipeTransform {
    transform(value: string): string {
        return value.replace(' ', '-').toLowerCase();
    }
}
