/*
 * // tslint:disable-next-line: no-reference
///<reference path="../../assets/js/jsmind.d.ts" />
import { Component, OnInit, Inject, ViewChild, ElementRef, Output, EventEmitter } from '@angular/core';
import { WindowRef } from '../windowref';
import { AuthService } from '../auth.service';
import { GraphService } from '../graph.service';
import { jsMind } from '../../assets/js/jsmind';

import mindmap_cloud from '../../assets/data/mindmap-cloud.json';

@Component({
  selector: 'app-mindmap',
  templateUrl: './mindmap.component.html',
  styleUrls: ['./mindmap.component.scss'],
  providers: [ WindowRef, AuthService, GraphService ],
})
export class MindmapComponent implements OnInit {
  @ViewChild('jsmind_container') container: ElementRef;

  private jm: jsMind;
  private selectedSkills: string[] = [];

  @Output() loaded = new EventEmitter<any>();

  constructor(
    private winRef: WindowRef) { }

  ngOnInit(): void {
    this.init_jsMind();
    this.load_mind();
    this.register_event();

    this.loaded.emit();
  }

  public get SelectedSkills(): string[] {
    return this.selectedSkills;
  }

  private init_jsMind() {
    const options = {
        editable: true,
        container: 'jsmind_container',
        theme: 'mstack'
    };

    const jsMindType: any = this.winRef.nativeWindow.jsMind;
    this.jm = new jsMindType(options);
    this.jm.init();
  }

  public load_mind(subset?: string) {
    // when the #mindmap-mapname anchor has been set, load that map when no other was specified
    if (! subset && window.location.hash) {
        const hash = window.location.hash.substr(1);
        if (hash.startsWith('mindmap-')) {
            subset = hash;
        }
    }

    // defaults back to the "cloud" mindmap when none is set
    subset = subset || 'mindmap-cloud';

    switch (subset) {
      case 'mindmap-cloud': this.jm.show(mindmap_cloud); break;
      case 'mindmap-development': this.jm.show(mindmap_development); break;
      case 'mindmap-marktgebieden': this.jm.show(mindmap_marktgebieden); break;
      case 'mindmap-projectaanpak': this.jm.show(mindmap_projectaanpak); break;
      case 'mindmap-softskills': this.jm.show(mindmap_softskills); break;
      case 'mindmap-vakgebied': this.jm.show(mindmap_vakgebied); break;
    }
  }

  private register_event() {
    this.jm.add_event_listener(async (type, data) => {
        if (type === 6) {
            await this.toggleSkill(data.node);
            this.updateCSS();
        }
    });
  }

  public clearSelection() {
    this.selectedSkills = [];
    this.updateCSS();
  }

  public addSkill(skill) {
    if (!this.selectedSkills.includes(skill)) {
        this.selectedSkills.push(skill);
    }
    this.updateCSS();
  }

  private removeSkill(skill) {
    if (this.selectedSkills.includes(skill)) {
        const index = this.selectedSkills.indexOf(skill);
        this.selectedSkills.splice(index, 1);
    }
    this.updateCSS();
  }

  public toggleSkill(skill) {
    if (this.selectedSkills.includes(skill)) {
        this.removeSkill(skill);
    } else {
        this.addSkill(skill);
    }
  }

  private updateCSS() {
    const skills = this.selectedSkills;
    const skillsSheetElement: any = document.getElementById('skills-sheet');

    skillsSheetElement.innerHTML = '';

    const skillsSheet = skillsSheetElement.sheet;

    while (skillsSheet.cssRules.length > 0) {
        skillsSheet.deleteRule(0);
    }

    for (let i = 0; i < skills.length; i++) {
        const skill = skills[i];

        let styles0 = 'jmnodes jmnode[nodeid="' + skill + '"], jmnodes jmnode[nodeid="' + skill + '"].selected {';
        styles0 += 'background-color: #2fa0d6;';
        styles0 += '}';

        console.log('adding style ' + i + ' : ' + styles0);
        skillsSheet.insertRule(styles0, i);
    }
}
}
*/