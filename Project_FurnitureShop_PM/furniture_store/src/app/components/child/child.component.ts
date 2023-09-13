import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-child',
  templateUrl: './child.component.html',
  styleUrls: ['./child.component.css']
})
export class ChildComponent implements OnChanges {
  @Input() userName = '';
  
  ngOnChanges(changes:SimpleChanges) {
    console.log('ngOnChanges triggered', changes);
 }
}