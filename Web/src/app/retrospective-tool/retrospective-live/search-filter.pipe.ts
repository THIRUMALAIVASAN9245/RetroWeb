import { Injectable, Pipe, PipeTransform } from '@angular/core';
import { AgilePointDetails } from '../models/agile-point-details';

@Pipe({
  name: 'searchfilter'
})

@Injectable()
export class SearchFilterPipe implements PipeTransform { 
  transform(items: AgilePointDetails[], filter: any): any[] {
    if (!items || !filter) {
      return items;
  }
  return items.filter(item => item.Text.toLowerCase().indexOf(filter.toLowerCase()) !== -1);
  }
}