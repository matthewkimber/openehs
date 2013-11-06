/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-12-2011
 * 
 * Author: Cameron Harp (charp5257@gmail.com)
 *****************************************************************************/

using System;
using System.Collections.Generic;

namespace OpenEhs.Domain
{
    /// <summary>
    /// Patient represents a patient with all their details
    /// </summary>
    public class Patient : IEntity
    {
        #region Properties

        /// <summary>
        /// Id of the patient
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// First Name of the Patient
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Middle Name of the Patient
        /// </summary>
        public virtual string MiddleName { get; set; }

        /// <summary>
        /// Last Name of the Patient
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Patient's Date of Birth
        /// </summary>
        public virtual DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Patient's age
        /// </summary>
        public virtual int Age
        {
            get 
            { 
                var age = DateTime.Now.Subtract(DateOfBirth);
                return age.Days/365;
            }
        }

        /// <summary>
        /// Patient's Gender
        /// </summary>
        public virtual Gender Gender { get; set; }

        /// <summary>
        /// Patient's Phone Number
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// Patient's Emergency Contact
        /// </summary>
        public virtual EmergencyContact EmergencyContact { get; set; }

        /// <summary>
        /// Patient's Address
        /// </summary>
        public virtual Address Address { get; set; }

        /// <summary>
        /// The blood type of the patient
        /// </summary>
        public virtual BloodTypes BloodType { get; set; }

        /// <summary>
        /// The tribe of the patient
        /// </summary>
        public virtual Tribes Tribe { get; set; }

        /// <summary>
        /// Patient's race
        /// </summary>
        public virtual Races Race { get; set; }

        /// <summary>
        /// Patient's religion
        /// </summary>
        public virtual Religions Religion { get; set; }

        /// <summary>
        /// Patient's level of education
        /// </summary>
        public virtual Education Education { get; set; }

        /// <summary>
        /// The occupation of the patient
        /// </summary>
        public virtual string Occupation { get; set; }

        /// <summary>
        /// patient's national insurance number
        /// </summary>
        public virtual string InsuranceNumber { get; set; }

        /// <summary>
        /// the date of expiration for the patient's insurance 
        /// </summary>
        public virtual DateTime InsuranceExpiration { get; set; }

        /// <summary>
        /// Notes on the patient
        /// </summary>
        public virtual string Note { get; set; }

        /// <summary>
        /// The patient's old physical record number from their file
        /// </summary>
        public virtual string OldPhysicalRecordNumber { get; set; }

        /// <summary>
        /// Date of the patient's death
        /// </summary>
        public virtual DateTime DateOfDeath { get; set; }

        /// <summary>
        /// Date that the electronic record was created
        /// </summary>
        public virtual DateTime CreationDate { get; set; }

        /// <summary>
        /// The location of the patients birth
        /// </summary>
        public virtual string PlaceOfBirth { get; set; }

        /// <summary>
        /// The patient's marital status
        /// </summary>
        public virtual MaritalStatus MaritalStatus { get; set; }

        /// <summary>
        /// Whether or not the patient is active
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// List of the patients check ins
        /// </summary>
        public virtual IList<PatientCheckIn> PatientCheckIns { get; set; }

        /// <summary>
        /// List of the patients problems
        /// </summary>
        public virtual IList<PatientProblem> Problems { get; set; }

        /// <summary>
        /// List of the patient's allergies
        /// </summary>
        public virtual IList<PatientAllergy> Allergies { get; set; }

        /// <summary>
        /// List of the medication prescribed to the patient
        /// </summary>
        public virtual IList<PatientMedication> Medications { get; set; }

        /// <summary>
        /// List of immunizations that have been administered to this patient
        /// </summary>
        public virtual IList<PatientImmunization> Immunizations { get; set; }

        #endregion
    }
}
