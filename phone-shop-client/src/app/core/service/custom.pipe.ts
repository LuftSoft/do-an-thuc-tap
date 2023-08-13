import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'vndPipe' })
export class VNDPipe implements PipeTransform {
    transform(value: number, exponent = 0): string {
        let result = '';
        if (value === 0) return '0 VNĐ'
        while (value > 0) {
            let tmp = value % 1000;
            result = `${tmp == 0 ? '000' : tmp.toString()}${result == '' ? '' : '.' + result}`;
            value = Math.floor(value / 1000);
        }
        return `${result} VNĐ`;
    }
}


@Pipe({ name: 'adminVndPipe' })
export class AdminVNDPipe implements PipeTransform {
    transform(value: number, exponent = 0): string {
        let result = '';
        if (value === 0) return '0 VNĐ'
        while (value > 0) {
            let tmp = value % 1000;
            result = `${tmp == 0 ? '000' : tmp.toString()}${result == '' ? '' : '.' + result}`;
            value = Math.floor(value / 1000);
        }
        return `${result} VNĐ`;
    }
}