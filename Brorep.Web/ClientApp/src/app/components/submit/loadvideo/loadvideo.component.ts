import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, FormBuilder, FormGroup } from '@angular/forms';

@Component({
    selector: 'app-loadvideo',
    templateUrl: './loadvideo.component.html',
    styleUrls: ['./loadvideo.component.scss']
})
export class LoadVideoComponent implements OnInit {
    @Output() videoSelected = new EventEmitter();
    @Output() goBack = new EventEmitter();

    loadVideoForm: FormGroup;
    videoUrl: string;
    constructor(private fb: FormBuilder) {
        this.loadVideoForm = this.fb.group({
            videoUrl: new FormControl('')
          });
    }

    ngOnInit(): void { }

    onGoBackClick() {
        this.goBack.emit();
    }

    onLoadVideoFormSubmit() {
        this.videoUrl = this.loadVideoForm.get('videoUrl').value;
        this.videoSelected.emit(this.videoUrl);
    }
}
