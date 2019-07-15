export class RetroInfo {
    /**
     * Constructor for RetroInfo class
     * @param retroinfo_id
     * @param retroinfo_name
     * @param retroinfo_projectinfo_id
     * @param retroinfo_sprint
     * @param retroinfo_date
     * @param retroinfo_imagepath
     * @param retroinfo_status
    */
    constructor(
        public retroinfo_id: number,
        public retroinfo_status: boolean,
        public retroinfo_name: string,
        public retroinfo_projectinfo_id: string,
        public retroinfo_projectinfo_name: string,
        public retroinfo_sprint: string,
        public retroinfo_date: string,
        public retroinfo_imagepath: string
       ) { }
} 